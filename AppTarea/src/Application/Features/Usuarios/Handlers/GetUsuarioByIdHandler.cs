using Application.Features.Usuarios.Queries;
using Application.Features.Usuarios.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using AutoMapper;

namespace Application.Features.Usuarios.Handlers
{
    public class GetUsuarioByIdHandler : IRequestHandler<GetUsuarioByIdQuery, UsuarioDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public GetUsuarioByIdHandler(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioDTO> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(request.Id);
            if (usuario == null)
            {
                throw new KeyNotFoundException($"Usuario con ID {request.Id} no encontrado.");

            }
                
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);

            return usuarioDTO;
        }
    }
}
