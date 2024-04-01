namespace Dominio.Entities.GenericEntities;
    public class Usuario : BaseEntity {
        
        // Propiedades
        public string ? UserName { get; set; }
        public string ? Email { get; set; }
        public string ? Password { get; set; }
        public ICollection<Rol> ? Roles { get; set; } = new HashSet<Rol>();
        public ICollection<UsuarioRoles> ? UsuarioRoles { get; set; }
        public ICollection<RefreshToken> ? RefreshTokens { get; set; } = new HashSet<RefreshToken>();
        public ICollection<Tarea> ? Tareas { get; set; }

    }
