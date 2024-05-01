using Dominio.Entities.Generic;

namespace Dominio.Interfaces.Generic;
    public interface IUsuario : IGenericRepository<Usuario> {
        
        Task<Usuario> GetByUsernameAsync(string username);
        Task<Usuario> GetByRefreshTokenAsync(string username);

    }
