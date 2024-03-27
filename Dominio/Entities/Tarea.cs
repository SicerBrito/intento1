using Dominio.Entities.GenericEntities;

namespace Dominio.Entities;
    public class Tarea : BaseEntity{

        public int UsuarioId { get; set; }
        public Usuario ? Usuarios { get; set; }
        public string ? Titulo { get; set; }
        public string ? Descripcion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int CategoriaId { get; set; }
        public Categoria ? Categorias { get; set; }

    }
