using Dominio.Interfaces.Generic;

namespace Dominio.Interfaces;
    public interface IUnitOfWork{
        
        IUsuario ? Usuarios { get; }
        IRol ? Roles { get; }
        IUsuarioRoles ? UsuarioRoles { get; }

        Task<int> SaveAsync();

    }
