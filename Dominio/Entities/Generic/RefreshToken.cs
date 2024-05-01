namespace Dominio.Entities.Generic;
    public class RefreshToken : BaseEntity {

        // Propiedades
        public int UsuarioId { get; set; }
        public Usuario ? Usuarios { get; set; }
        public string ? Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires; // Verifica si el token ha expirado con la fecha y hora actual
        public DateTime Created { get; set; }
        public DateTime ? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired; // Verifica si el token de actualización está activo si no ha sido revocado y si no ha expirado
        
    }
