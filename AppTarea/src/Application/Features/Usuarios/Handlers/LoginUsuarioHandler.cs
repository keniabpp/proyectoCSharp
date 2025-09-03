using Application.Features.Usuarios.Commands;
using Application.Features.Usuarios.DTOs;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Features.Usuarios.Handlers
{
    public class LoginUsuarioHandler : IRequestHandler<LoginUsuarioCommand, UsuarioLoginResponseDTO?>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _config;

        public LoginUsuarioHandler(IUsuarioRepository usuarioRepository, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _config = config;
        }

        public async Task<UsuarioLoginResponseDTO?> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UsuarioLoginDTO;

            // Validar existencia del usuario
            var usuario = await _usuarioRepository.GetByEmailAsync(dto.Email);
            if (usuario == null) return null;

            // Validar contraseña
            if (!BCrypt.Net.BCrypt.Verify(dto.Contrasena, usuario.contrasena)) return null;

            

            var roleName = usuario.Rol?.nombre.ToLower() ?? "usuario";


            // Generar claims
            var claims = new List<Claim>
            {
                new Claim("id", usuario.id_usuario.ToString()),
                new Claim(ClaimTypes.NameIdentifier, usuario.id_usuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.nombre),
                new Claim(ClaimTypes.Email, usuario.email),
                new Claim(ClaimTypes.Role, roleName)
            };

            // Validar configuración JWT
            var jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("La clave JWT no está configurada.");
            var jwtIssuer = _config["Jwt:Issuer"]?? throw new InvalidOperationException("El issuer JWT no está configurado.");
            var jwtAudience = _config["Jwt:Audience"]?? throw new InvalidOperationException("El audience JWT no está configurado.");
            var jwtExpireMinutesString = _config["Jwt:ExpireMinutes"]?? throw new InvalidOperationException("El tiempo de expiración JWT no está configurado.");
            if (!double.TryParse(jwtExpireMinutesString, out double jwtExpireMinutes))
                throw new InvalidOperationException("El tiempo de expiración JWT no es un número válido.");

            // Generar la llave y credenciales
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));



            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crear token
            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtExpireMinutes),
                signingCredentials: creds
            );

            // Devolver DTO con token
            return new UsuarioLoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Nombre = usuario.nombre,
                Email = usuario.email,
                Rol = usuario.Rol?.nombre ?? "Usuario"
            };
        }
    }
}
