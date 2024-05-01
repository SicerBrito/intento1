using Dominio.Interfaces.Generic;

namespace Dominio.Interfaces.IUnitOfWork;
    public interface IUnitOfWork {
        
        IUsuario ? Usuarios { get; }
        IRol ? Roles { get; }
        IUsuarioRoles ? UsuarioRoles { get; }
        ICandidato ? Candidatos { get; }

        Task<int> SaveAsync();

    }
