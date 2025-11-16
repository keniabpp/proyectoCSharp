using Application.Features.Usuarios.Commands;
using Application.Features.Usuarios.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Features.Usuarios.Handlers
{
    public class CreateUsuarioHandler : IRequestHandler<CreateUsuarioCommand, UsuarioDTO>
    {
        private readonly IApplicationUserService _userService;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public CreateUsuarioHandler(IApplicationUserService userService, IMapper mapper, IRoleService roleService)
        {
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task<UsuarioDTO> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UsuarioCreateDTO;
            
            // Usar el método específico para Identity que maneja roles correctamente
            var result = await _userService.CreateUserWithRoleIdAsync(
                dto.Email,
                dto.Contrasena,
                dto.Nombre,
                dto.Apellido,
                dto.Telefono,
                dto.id_rol
            );
            
            if (!result.IsSuccess)
            {
                throw new InvalidOperationException($"Error al crear usuario: {string.Join(", ", result.Errors)}");
            }
            
            var usuarioDTO = _mapper.Map<UsuarioDTO>(result.User);
            
            // Poblar rolNombre
            usuarioDTO.rolNombre = await _roleService.GetRoleNameByIdAsync(usuarioDTO.id_rol) ?? string.Empty;
            
            return usuarioDTO;
        }
    }
}
