namespace Dominio.Entities.GenericEntities;
    public class UsuarioRoles : BaseEntity{
        
        // Propiedades
        public int UsuarioId { get; set; }
        public Usuario ? Usuarios { get; set; }
        public int RolId { get; set; }
        public Rol ? Roles { get; set; }

    }
