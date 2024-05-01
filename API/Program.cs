using System.Reflection;
using API.Extensions;
using API.Helpers;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .Enrich.FromLogContext()
                    .CreateLogger();

builder.Logging.AddSerilog(logger);

// El context accessor nos permite que podamos implementar la autorizacion de roles
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllers( options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true; // Envia error si no es soportado el formato que se quiere usar

}).AddXmlSerializerFormatters();



builder.Services.ConfigureCors(); // Configuración de las cors
builder.Services.AddAplicacionServices(); // Configuración de la UnitOfWork(repo-interface) y otras cosas mas
builder.Services.AddJwt(builder.Configuration); // Definir los parametros del JWT para añadir 
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly()); // Habilitar el AutoMapper
builder.Services.ConfigureRateLimiting(); // Habilitar la configuración del número de peticiones
builder.Services.ConfigureApiVersioning(); // Habilitar las versiones o versionado en el proyecto para las Apis



builder.Services.AddAuthorization(opts => {
    opts.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddRequirements(new GlobalVerbRoleRequirement())
        .Build();
});

// Habilitamos la conexion a la base de datos 
builder.Services.AddDbContext<DbAppContext>(options =>
{
    string ? connectionString = builder.Configuration.GetConnectionString("ConexMysqlHome");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Activar migraciones pendientes 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<DbAppContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var _logger = loggerFactory.CreateLogger<Program>();
        _logger.LogError(ex, "Ocurrió un error durante la migración");
    }
}



app.UseIpRateLimiting();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

