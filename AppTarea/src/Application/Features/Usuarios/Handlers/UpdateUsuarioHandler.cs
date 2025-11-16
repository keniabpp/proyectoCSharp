using Application.Features.Usuarios.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using AutoMapper;
using Application.Features.Usuarios.DTOs;
using Application.Interfaces;

namespace Application.Features.Usuarios.Handlers
{
    public class UpdateUsuarioHandler : IRequestHandler<UpdateUsuarioCommand, UsuarioDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public UpdateUsuarioHandler(IUsuarioRepository usuarioRepository, IMapper mapper, IRoleService roleService)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task<UsuarioDTO> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuarioExistente = await _usuarioRepository.GetByIdAsync(request.Id);

            if (usuarioExistente!.email != request.UsuarioUpdateDTO.Email)
            {
                var otroUsuario = await _usuarioRepository.GetByEmailAsync(request.UsuarioUpdateDTO.Email);
                if (otroUsuario != null && otroUsuario.id_usuario != request.Id)
                throw new Exception("El correo electrónico ya está en uso por otro usuario.");
            }

            _mapper.Map(request.UsuarioUpdateDTO, usuarioExistente);

            if (!string.IsNullOrEmpty(request.UsuarioUpdateDTO.Contrasena))
            {
                usuarioExistente.contrasena = BCrypt.Net.BCrypt.HashPassword(request.UsuarioUpdateDTO.Contrasena);  // Solo se actualiza si la contraseña está presente
            }

            await _usuarioRepository.UpdateAsync(request.Id, usuarioExistente);
            
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuarioExistente);
            
            // Poblar rolNombre
            usuarioDTO.rolNombre = await _roleService.GetRoleNameByIdAsync(usuarioDTO.id_rol) ?? string.Empty;
            
            return usuarioDTO;
        }
    }
}
