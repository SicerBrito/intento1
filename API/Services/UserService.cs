using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.Dtos.Generic;
using API.Helpers;
using Dominio.Entities.Generic;
using Dominio.Interfaces.IUnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;
    public class UserService : IUserService {

        private readonly JWT _Jwt;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IPasswordHasher<Usuario> _PasswordHasher;
        public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt, IPasswordHasher<Usuario> passwordHasher)
        {
            _Jwt = jwt.Value;
            _UnitOfWork = unitOfWork;
            _PasswordHasher = passwordHasher;
        }



        private RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(10),
                    Created = DateTime.UtcNow
                };
            }
        }



        async Task<string> IUserService.RegisterAsync(RegisterDto registerDto)
        {
            var usuario = new Usuario
            {
                Email = registerDto.Email,
                UserName = registerDto.Username,
            };

            usuario.Password = _PasswordHasher.HashPassword(usuario, registerDto.Password!);

            var usuarioExiste = _UnitOfWork.Usuarios!
                                            .Find(u => u.UserName!.ToLower() == registerDto.Username!.ToLower())
                                            .FirstOrDefault();

            if (usuarioExiste == null)
            {
                var rolPredeterminado = _UnitOfWork.Roles!
                                                    .Find(u => u.Nombre == Autorizacion.rol_predeterminado.ToString())
                                                    .First();
                try
                {
                    usuario.Roles!.Add(rolPredeterminado);
                    _UnitOfWork.Usuarios.Add(usuario);
                    await _UnitOfWork.SaveAsync();

                    return $"El Usuario {registerDto.Username} ha sido registrado exitosamente";
                }

                catch (Exception ex)
                {
                    var message = ex.Message;
                    return $"Error: {message}";
                }
            }
            else
            {
                return $"El usuario con {registerDto.Username} ya se encuentra resgistrado.";
            }

        }



        async Task<string> IUserService.AddRoleAsync(AddRolDto model)
        {
            var usuario = await _UnitOfWork.Usuarios!
                                            .GetByUsernameAsync(model.Username!);

            if (usuario == null)
            {
                return $"No existe algun usuario registrado con la cuenta olvido algun caracter?{model.Username}.";
            }

            var resultado = _PasswordHasher.VerifyHashedPassword(usuario, usuario.Password!, model.Password!);

            if (resultado == PasswordVerificationResult.Success)
            {
                var rolExiste = _UnitOfWork.Roles!
                                            .Find(u => u.Nombre!.ToLower() == model.Rol!.ToLower())
                                            .FirstOrDefault();

                if (rolExiste != null)
                {
                    var usuarioTieneRol = usuario.Roles!
                                                    .Any(u => u.Id == rolExiste.Id);

                    if (usuarioTieneRol == false)
                    {
                        usuario.Roles!.Add(rolExiste);
                        _UnitOfWork.Usuarios.Update(usuario);
                        await _UnitOfWork.SaveAsync();
                    }

                    return $"Rol {model.Rol} agregado a la cuenta {model.Username} de forma exitosa.";
                }

                return $"Rol {model.Rol} no encontrado.";
            }

            return $"Credenciales incorrectas para el ususario {usuario.UserName}.";

        }



        public async Task<DataUserDto> GetTokenAsync(LoginDto model)
        {
            DataUserDto datosUsuarioDto = new DataUserDto();
            var usuario = await _UnitOfWork.Usuarios!
                                            .GetByUsernameAsync(model.Username!);

            if (usuario == null)
            {
                datosUsuarioDto.IsAuthenticated = false;
                datosUsuarioDto.Message = $"No existe ningun usuario con el username {model.Username}.";
                return datosUsuarioDto;
            }

            var result = _PasswordHasher.VerifyHashedPassword(usuario, usuario.Password!, model.Password!);
            if (result == PasswordVerificationResult.Success)
            {
                datosUsuarioDto.IsAuthenticated = true;
                datosUsuarioDto.Message = "OK";
                datosUsuarioDto.IsAuthenticated = true;

                if (usuario != null && usuario != null)
                {
                    JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
                    datosUsuarioDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                    datosUsuarioDto.UserName = usuario.UserName;
                    datosUsuarioDto.Email = usuario.Email;
                    datosUsuarioDto.Roles = (usuario.Roles!
                                                    .Select(p => p.Nombre)
                                                    .ToList())!;


                    if (usuario.RefreshTokens!.Any(a => a.IsActive))
                        {
                            var activeRefreshToken = usuario.RefreshTokens!.Where(a => a.IsActive == true).FirstOrDefault();
                            datosUsuarioDto.RefreshToken = activeRefreshToken!.Token;
                            datosUsuarioDto.RefreshTokenExpiration = activeRefreshToken.Expires;
                        }
                        else
                        {
                            var refreshToken = CreateRefreshToken();
                            datosUsuarioDto.RefreshToken = refreshToken.Token;
                            datosUsuarioDto.RefreshTokenExpiration = refreshToken.Expires;
                            usuario.RefreshTokens!.Add(refreshToken);
                            _UnitOfWork.Usuarios.Update(usuario);
                            await _UnitOfWork.SaveAsync();
                        }

                        return datosUsuarioDto;

                }
                else{
                    datosUsuarioDto.IsAuthenticated = false;
                    datosUsuarioDto.Message = $"Credenciales incorrectas para el usuario {usuario!.UserName}.";

                    return datosUsuarioDto;
                }
            }

            // Valor de retorno predeterminado en caso de que ninguna condición se cumpla
            return datosUsuarioDto;

        }



        // Metodo para la creacion del token Personalizado para cada tipo de rol
        private JwtSecurityToken CreateJwtToken(Usuario usuario)
        {
            var roles = usuario.Roles;
            var roleClaims = new List<Claim>();
            foreach (var role in roles!)
            {
                roleClaims.Add(new Claim("roles", role.Nombre!));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", usuario.Id.ToString())
            }
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Jwt.Key!));
            Console.WriteLine("", symmetricSecurityKey);

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var JwtSecurityToken = new JwtSecurityToken(
                issuer : _Jwt.Issuer,
                audience : _Jwt.Audience,
                claims : claims,
                expires : DateTime.UtcNow.AddMinutes(_Jwt.DurationInMinutes),
                signingCredentials : signingCredentials);

            return JwtSecurityToken;
        }



        async Task<DataUserDto> IUserService.RefreshTokenAsync(string refreshToken)
        {
            var datosUsuarioDto = new DataUserDto();

            var usuario = await _UnitOfWork.Usuarios!
                                            .GetByRefreshTokenAsync(refreshToken);

            if (usuario == null)
            {
                datosUsuarioDto.IsAuthenticated = false;
                datosUsuarioDto.Message = $"El token no esta asignado a ningun usuario.";
                return datosUsuarioDto;
            }

            var refreshTokenBd = usuario.RefreshTokens!.Single(x => x.Token == refreshToken);

            if (!refreshTokenBd.IsActive)
            {
                datosUsuarioDto.IsAuthenticated = false;
                datosUsuarioDto.Message = $"El token no es valido.";
                return datosUsuarioDto;
            }

            // Revoque el token de actualización actual y
            refreshTokenBd.Revoked = DateTime.UtcNow;
            // genera un nuevo token de actualización y lo guarda en la base de datos
            var newRefreshToken = CreateRefreshToken();
            usuario.RefreshTokens!.Add(newRefreshToken);
            _UnitOfWork.Usuarios.Update(usuario);
            await _UnitOfWork.SaveAsync();
            // Generar un nuevo Json Web Token
            datosUsuarioDto.IsAuthenticated = true;
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
            datosUsuarioDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            datosUsuarioDto.Email = usuario.Email;
            datosUsuarioDto.UserName = usuario.UserName;
            datosUsuarioDto.Roles = (usuario.Roles!
                                        .Select(u => u.Nombre)
                                        .ToList())!;
            datosUsuarioDto.RefreshToken = newRefreshToken.Token;
            datosUsuarioDto.RefreshTokenExpiration = newRefreshToken.Expires;
            return datosUsuarioDto;
        }



        // Editar el usuario registrado
        public async Task<Usuario> EditarUsuarioAsync(Usuario model)
        {
            Usuario usuario = new Usuario();
            usuario.Id = model.Id;
            usuario.UserName = model.UserName;
            usuario.Email = model.Email;
            usuario.Password = _PasswordHasher.HashPassword(usuario, model.Password!);
            _UnitOfWork.Usuarios!.Update(usuario);
            await _UnitOfWork.SaveAsync();
            return usuario;
        }

    }
