using Dominio.Entities.Generic;

namespace Dominio.Entities;
    public class Candidato : BaseEntity {

        public string ? NombreCompleto { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string ? Genero { get; set; }
        public string ? Telefono { get; set; }
        public string ? CiudadRecidencia { get; set; }
        public string ? Email { get; set; }
        public string ? DescripcionHabilidades { get; set; }
        public string ? Experiencia { get; set; }
        public string ? UrlLinkedIn { get; set; }

    }
