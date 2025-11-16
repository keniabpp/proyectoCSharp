using Application.Features.Usuarios.Queries;
using Application.Features.Usuarios.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using AutoMapper;
using Application.Interfaces;

namespace Application.Features.Usuarios.Handlers
{
    public class GetUsuarioByIdHandler : IRequestHandler<GetUsuarioByIdQuery, UsuarioDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public GetUsuarioByIdHandler(IUsuarioRepository usuarioRepository, IMapper mapper, IRoleService roleService)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task<UsuarioDTO> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(request.Id);
                
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);

            // Poblar rolNombre
            usuarioDTO.rolNombre = await _roleService.GetRoleNameByIdAsync(usuarioDTO.id_rol) ?? string.Empty;

            return usuarioDTO;
        }
    }
}
