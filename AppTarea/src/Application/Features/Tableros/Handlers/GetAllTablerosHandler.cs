using Application.Features.Tableros.DTOs;
using Application.Features.Tableros.Queries;
using Domain.Interfaces;
using MediatR;
using AutoMapper;
using Application.Interfaces;

namespace Application.Features.Tableros.Handlers
{
    public class GetAllTablerosHandler : IRequestHandler<GetAllTablerosQuery, IEnumerable<TableroDTO>>
    {
        private readonly ITableroRepository _tableroRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationUserService _userService;
        private readonly IRoleService _roleService;

        public GetAllTablerosHandler(ITableroRepository tableroRepository, IMapper mapper, IApplicationUserService userService, IRoleService roleService)
        {
            _tableroRepository = tableroRepository;
            _mapper = mapper;
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<IEnumerable<TableroDTO>> Handle(GetAllTablerosQuery request, CancellationToken cancellationToken)
        {
            var tableros = await _tableroRepository.GetAllAsync();

            var tableroDTO = _mapper.Map<IEnumerable<TableroDTO>>(tableros).ToList();

            // Poblar nombre_usuario y nombre_rol desde AspNetUsers y AspNetRoles
            foreach (var dto in tableroDTO)
            {
                // Obtener nombre del usuario
                dto.nombre_usuario = await _userService.GetUserNameByIdAsync(dto.creado_por);

                // Obtener nombre del rol
                dto.nombre_rol = await _roleService.GetRoleNameByIdAsync(dto.id_rol);
            }

            return tableroDTO;
        }
    }
}
