using API.Dtos.Generic;
using Dominio.Entities.Generic;

namespace API.Services;
    public interface IUserService {

        Task<string> RegisterAsync(RegisterDto model);
        Task<DataUserDto> GetTokenAsync(LoginDto model);
        Task<string> AddRoleAsync(AddRolDto model);
        Task<Usuario> EditarUsuarioAsync(Usuario model);
        Task<DataUserDto> RefreshTokenAsync(string refreshToken);

    }
