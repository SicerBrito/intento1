using Dominio.Entities.Generic;

namespace Dominio.Entities;
    public class Categoria : BaseEntity {

        public string ? Nombre { get; set; }
        public ICollection<Tarea> ? Tareas { get; set; }

    }
