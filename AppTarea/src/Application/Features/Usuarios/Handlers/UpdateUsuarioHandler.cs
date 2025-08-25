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
            if (usuarioExistente == null)
            {
                throw new Exception($" No se encontró el Usuario con ID {request.Id}");
            }

            if (usuarioExistente.email != request.UsuarioUpdateDTO.email)
            {
                var otroUsuario = await _usuarioRepository.GetByEmailAsync(request.UsuarioUpdateDTO.email);
                if (otroUsuario != null)
                    throw new Exception("El correo electrónico ya está en uso por otro usuario.");
            }

            _mapper.Map(request.UsuarioUpdateDTO, usuarioExistente);

            usuarioExistente.contrasena = BCrypt.Net.BCrypt.HashPassword(usuarioExistente.contrasena);

            await _usuarioRepository.UpdateAsync(request.Id, usuarioExistente);
            
            return _mapper.Map<UsuarioDTO>(usuarioExistente);
        }
    }
}
