namespace Dominio.Entities.Generic;
    public class Rol : BaseEntity {
        
        // Propiedades
        public string ? Nombre { get; set; }
        public virtual ICollection<Usuario> ? Usuarios { get; set; } = new HashSet<Usuario>();
        public ICollection<UsuarioRoles> ? UsuarioRoles { get; set; }

    }
