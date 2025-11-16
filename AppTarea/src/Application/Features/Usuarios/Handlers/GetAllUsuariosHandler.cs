using Application.Features.Usuarios.Queries;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using AutoMapper;
using Application.Features.Usuarios.DTOs;
using Application.Interfaces;

namespace Application.Features.Usuarios.Handlers
{
    public class GetAllUsuariosHandler : IRequestHandler<GetAllUsuariosQuery, IEnumerable<UsuarioDTO>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public GetAllUsuariosHandler(IUsuarioRepository usuarioRepository, IMapper mapper, IRoleService roleService)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task<IEnumerable<UsuarioDTO>> Handle(GetAllUsuariosQuery request, CancellationToken cancellationToken)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            var usuarioDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios).ToList();

            // Poblar rolNombre
            foreach (var dto in usuarioDTO)
            {
                dto.rolNombre = await _roleService.GetRoleNameByIdAsync(dto.id_rol) ?? string.Empty;
            }

            return usuarioDTO;
        }

        
    }
}
