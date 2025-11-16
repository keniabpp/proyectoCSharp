using Application.Features.Usuarios.Commands;
using Domain.Entities;
using Domain.Interfaces;
using AutoMapper;
using MediatR;
using Application.Features.Usuarios.DTOs;
using Application.Interfaces;

namespace Application.Features.Usuarios.Handlers
{
    public class RegisterUsuarioCommandHandler : IRequestHandler<RegisterUsuarioCommand, UsuarioRegisterResponseDTO>
    {
        private readonly IApplicationUserService _userService;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public RegisterUsuarioCommandHandler(IApplicationUserService userService, IMapper mapper, IRoleService roleService)
        {
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task<UsuarioRegisterResponseDTO> Handle(RegisterUsuarioCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UsuarioRegisterDTO;
            
            // Usar el método específico para Identity que maneja roles correctamente
            // Rol "Usuario" (ID = 2) por defecto para registro público
            var result = await _userService.CreateUserWithRoleIdAsync(
                dto.Email,
                dto.Contrasena,
                dto.Nombre,
                dto.Apellido,
                dto.Telefono,
                2 // Rol "Usuario" por defecto
            );
            
            if (!result.IsSuccess)
            {
                throw new InvalidOperationException($"Error al registrar usuario: {string.Join(", ", result.Errors)}");
            }
            
            var usuarioRegisterResponseDTO = _mapper.Map<UsuarioRegisterResponseDTO>(result.User);
            
            // Poblar rolNombre
            usuarioRegisterResponseDTO.rolNombre = await _roleService.GetRoleNameByIdAsync(usuarioRegisterResponseDTO.id_rol) ?? string.Empty;
            
            return usuarioRegisterResponseDTO;
        }
    }
}