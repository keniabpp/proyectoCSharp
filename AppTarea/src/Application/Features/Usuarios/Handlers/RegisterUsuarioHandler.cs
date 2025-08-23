using Application.Features.Usuarios.Commands;
using Domain.Entities;
using Domain.Interfaces;
using AutoMapper;
using MediatR;
using Application.Features.Usuarios.DTOs;

namespace Application.Features.Usuarios.Handlers
{
    public class RegisterUsuarioCommandHandler : IRequestHandler<RegisterUsuarioCommand, UsuarioDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public RegisterUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioDTO> Handle(RegisterUsuarioCommand request, CancellationToken cancellationToken)
        {

            // 1. Validar email único
            var usuarioExistente = await _usuarioRepository.GetByEmailAsync(request.UsuarioRegisterDTO.email);
            if (usuarioExistente != null)
            throw new InvalidOperationException("El correo electrónico ya está registrado.");
            var usuario = _mapper.Map<Usuario>(request.UsuarioRegisterDTO);
            // Asignar rol fijo de usuario normal
            usuario.id_rol = 2; // 2 = usuario normal
            usuario.contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.contrasena);
            var usuarioRegistrado = await _usuarioRepository.CreateAsync(usuario);
            var usuarioRegisterDTO = _mapper.Map<UsuarioDTO>(usuarioRegistrado);

            return usuarioRegisterDTO;
        }
    }
}
