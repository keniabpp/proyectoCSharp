using Application.Features.Usuarios.Commands;
using Application.Features.Usuarios.DTOs;
using Application.Features.Tareas.Queries;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Features.Usuarios.Handlers
{
    /// <summary>
    /// Handler de login que usa ASP.NET Core Identity para autenticación
    /// Mantiene la misma interfaz pero usa las nuevas capacidades de Identity
    /// </summary>
    public class IdentityLoginUsuarioHandler : IRequestHandler<LoginUsuarioCommand, UsuarioLoginResponseDTO?>
    {
        private readonly IApplicationUserService _userService;
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;

        public IdentityLoginUsuarioHandler(IApplicationUserService userService, IConfiguration config, IMediator mediator)
        {
            _userService = userService;
            _config = config;
            _mediator = mediator;
        }

        public async Task<UsuarioLoginResponseDTO?> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UsuarioLoginDTO;

            // Obtener el usuario
            var usuario = await _userService.GetByEmailAsync(dto.Email);
            if (usuario == null) return null;

            // Validar contraseña con manejo de lockout
            var validationResult = await _userService.ValidatePasswordAsync(dto.Email, dto.Contrasena);
            
            if (!validationResult.IsSuccess)
            {
                if (validationResult.IsLockedOut)
                {
                    throw new UnauthorizedAccessException(validationResult.ErrorMessage ?? "Cuenta bloqueada");
                }
                return null;
            }

            // Obtener roles usando Identity
            var roles = await _userService.GetRolesAsync(usuario.id_usuario);
            var roleName = roles.FirstOrDefault() ?? "Usuario";

            // Generar claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.id_usuario.ToString()),
                new Claim(ClaimTypes.Email, usuario.email),
                new Claim(ClaimTypes.Name, $"{usuario.nombre} {usuario.apellido}"),
                new Claim(ClaimTypes.Role, roleName),
                new Claim("nombre", usuario.nombre),
                new Claim("apellido", usuario.apellido),
                new Claim("telefono", usuario.telefono)
            };

            // Agregar roles de Identity como claims adicionales
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Generar token JWT
            var jwtSettings = _config.GetSection("Jwt");
            var keyBytes = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
            var key = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"] ?? "60")),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Obtener tareas asignadas al usuario
            var tareasQuery = new GetTareasAsignadasQuery(usuario.id_usuario);
            var tareasAsignadas = await _mediator.Send(tareasQuery, cancellationToken);

            return new UsuarioLoginResponseDTO
            {
                id_usuario = usuario.id_usuario,
                Nombre = usuario.nombre,
                Email = usuario.email,
                Rol = roleName,
                Token = tokenString
            };
        }
    }
}