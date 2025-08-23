using Application.Features.Usuarios.Queries;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using AutoMapper;
using Application.Features.Usuarios.DTOs;

namespace Application.Features.Usuarios.Handlers
{
    public class GetAllUsuariosHandler : IRequestHandler<GetAllUsuariosQuery, IEnumerable<UsuarioDTO>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public GetAllUsuariosHandler(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioDTO>> Handle(GetAllUsuariosQuery request, CancellationToken cancellationToken)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            var usuarioDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);

            return usuarioDTO;
        }

        
    }
}
