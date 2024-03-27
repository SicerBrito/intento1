namespace Dominio.Entities.GenericEntities;
    public class Rol : BaseEntity{
        
        // Propiedades
        public string ? Nombre { get; set; }
        public ICollection<Usuario> ? Usuarios { get; set; } = new HashSet<Usuario>();
        public ICollection<UsuarioRoles> ? UsuarioRoles { get; set; }

    }
