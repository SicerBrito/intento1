using System.Text;
using API.Helpers;
using API.Services;
using Aplicacion.UnitOfWork;
using AspNetCoreRateLimit;
using Dominio.Entities.Generic;
using Dominio.Interfaces.IUnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

    public static class ApplicationServiceExtension{

        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",builder=>
                    builder.AllowAnyOrigin()        //WithOrigins(http://domini.com)
                    .AllowAnyMethod()               //WithMethods(*GET*, *POST*)
                    .AllowAnyHeader());             //WithHeaders(*accept*, content-type)
            });


        public static void AddAplicacionServices(this IServiceCollection services){
            services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IAuthorizationHandler, GlobalVerbRoleHandler>();
        }

        // Definimos la configuración del JWT
        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración desde AppSettings
            services.Configure<JWT>(configuration.GetSection("JWT"));

            // Añadimos Autenticación - JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
                    };
                });
        }

        // Definimos el limite de peticiones que podemos realizar a un EndPoint
        public static void ConfigureRateLimiting(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddInMemoryRateLimiting();
            services.Configure<IpRateLimitOptions>(options => 
            {
                options.StackBlockedRequests = false;
                options.HttpStatusCode = 429;
                options.RealIpHeader = "X-Real-IP";
                options.GeneralRules = new List<RateLimitRule>
                {
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        Period = "10s",
                        Limit = 10
                    }
                };

            });

        }

        // Control de versiones de Apis (Ver versiones de las Apis creadas o Enpoints)
        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options => {

                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new QueryStringApiVersionReader("v");
                options.ApiVersionReader = new HeaderApiVersionReader("X-Version");
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("v"),
                    new HeaderApiVersionReader("X-Version")
                );
                options.ReportApiVersions = true;

            });
        }

    }