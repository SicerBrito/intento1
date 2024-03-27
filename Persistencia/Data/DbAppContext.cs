using System.Reflection;
using Dominio.Entities;
using Dominio.Entities.GenericEntities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia.Data;
    public class DbAppContext : DbContext{

        // Constructor: Asegura que esté listo para interactuar con la base de datos
        public DbAppContext(DbContextOptions<DbAppContext> options) : base (options){

        }

        // Colección de entidades en la base de datos
        public DbSet<Usuario> ? Usuarios { get; set; } = null!;
        public DbSet<Rol> ? Roles { get; set; } = null!;
        public DbSet<UsuarioRoles> ? UsuarioRoles { get; set; } = null!;
        public DbSet<RefreshToken> ? RefreshTokens { get; set; } = null!;
        public DbSet<Tarea> ? Tareas { get; set; } = null!;
        public DbSet<Categoria> ? Categorias { get; set; } = null!;


        // Configurar el modelo de datos, es decir, definir cómo se mapean las entidades a las tablas de la base de datos y cómo se configuran sus relaciones y propiedades
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }