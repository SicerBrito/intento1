using Dominio.Interfaces.Generic;

namespace Dominio.Interfaces.IUnitOfWork;
    public interface IUnitOfWork {
        
        IUsuario ? Usuarios { get; }
        IRol ? Roles { get; }
        IUsuarioRoles ? UsuarioRoles { get; }
        ICategoria ? Categorias { get; }
        ITarea ? Tareas { get; }

        Task<int> SaveAsync();

    }