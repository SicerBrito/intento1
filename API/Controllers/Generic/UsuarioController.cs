using API.Dtos.Generic;
using API.Services;
using AutoMapper;
using Dominio.Interfaces.IUnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Generic;
    public class UsuarioController : BaseApiController {

        private readonly IUnitOfWork _UnitOfWork;
        private readonly IUserService _UserService;
        private readonly IMapper _Mapper;

        
        // Constructor de las Clases
        public UsuarioController(IUnitOfWork unitOfWork, IUserService userService, IMapper mapper) {

            _UnitOfWork = unitOfWork;
            _UserService = userService;
            _Mapper = mapper;

        }


        // METODO POST Para agregar nuevos Usuarios
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RegisterAsync(RegisterDto model)
        {
            var result = await _UserService.RegisterAsync(model);
            return Ok(result);
        }


        //METODO POST Para obtener el Token con su respectivo RefreshToken
        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTokenAsync(LoginDto model)
        {
            var result = await _UserService.GetTokenAsync(model);
            SetRefreshTokenInCookie(result.RefreshToken!); // Activar la cookies con el RefreshToken
            return Ok(result);
        }

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }


        // METODO POST Para añadirle un Rol al Usuario 
        [HttpPost("addrole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddRoleAsync(AddRolDto model)
        {
            var result = await _UserService.AddRoleAsync(model);
            return Ok(result);
        }


        // METODO POST Para obtener el RefreshToken y actualizarlo
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _UserService.RefreshTokenAsync(refreshToken!);

            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);

            return Ok(response);
        }

    }
