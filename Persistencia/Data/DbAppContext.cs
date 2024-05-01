using System.Reflection;
using Dominio.Entities;
using Dominio.Entities.Generic;
using Microsoft.EntityFrameworkCore;

namespace Persistencia.Data;
    public class DbAppContext : DbContext {
        
        // Constructor: Asegura que esté listo para interactuar con la base de datos
        public DbAppContext(DbContextOptions<DbAppContext> options) : base (options) {

        }

        // Colección de entidades en la base de datos
        public DbSet<Usuario> ? Usuarios { get; set; } = null!;
        public DbSet<Rol> ? Roles { get; set; } = null!;
        public DbSet<UsuarioRoles> ? UsuarioRoles { get; set; } = null!;
        public DbSet<RefreshToken> ? RefreshTokens { get; set; } = null!;
        public DbSet<Candidato> ? Candidatos { get; set; } = null!;


        // Configurar el modelo de datos, es decir, definir cómo se mapean las entidades a las tablas de la base de datos y cómo se configuran sus relaciones y propiedades
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.EnableSensitiveDataLogging().UseMySql("Server=localhost;Port=3306;Database=myDataBase;Uid=campus2023;Pwd=campus2023;", new MySqlServerVersion(new Version(8, 0, 28)));
            }
        }

    }


