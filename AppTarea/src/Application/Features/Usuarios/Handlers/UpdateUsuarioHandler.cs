using Application.Features.Usuarios.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using AutoMapper;
using Application.Features.Usuarios.DTOs;

namespace Application.Features.Usuarios.Handlers
{
    public class UpdateUsuarioHandler : IRequestHandler<UpdateUsuarioCommand, UsuarioDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UpdateUsuarioHandler(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
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
            
            return _mapper.Map<UsuarioDTO>(usuarioExistente);
        }
    }
}
