using Application.Features.Usuarios.Commands;
using Application.Features.Usuarios.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Usuarios.Handlers
{
    public class CreateUsuarioHandler : IRequestHandler<CreateUsuarioCommand, UsuarioDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public CreateUsuarioHandler(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioDTO> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = _mapper.Map<Usuario>(request.UsuarioCreateDTO);
            // Encriptar la contraseña después del mapeo
            usuario.contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.contrasena);
            var usuarioCreado = await _usuarioRepository.CreateAsync(usuario);
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuarioCreado);
            return usuarioDTO;
        }
    }
}
