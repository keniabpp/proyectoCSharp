using Application.Features.Tableros.DTOs;
using Application.Features.Tableros.Queries;
using Domain.Interfaces;
using MediatR;
using AutoMapper;
using Application.Interfaces;

namespace Application.Features.Tableros.Handlers
{
    public class GetTableroByIdHandler : IRequestHandler<GetTableroByIdQuery, TableroDTO>
    {
        private readonly ITableroRepository _tableroRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationUserService _userService;
        private readonly IRoleService _roleService;

        public GetTableroByIdHandler(ITableroRepository tableroRepository, IMapper mapper, IApplicationUserService userService, IRoleService roleService)
        {
            _tableroRepository = tableroRepository;
            _mapper = mapper;
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<TableroDTO> Handle(GetTableroByIdQuery request, CancellationToken cancellationToken)
        {
            var tablero = await _tableroRepository.GetByIdAsync(request.Id);


            if (tablero == null)
                throw new KeyNotFoundException("Tablero no encontrado.");
            var tableroDTO = _mapper.Map<TableroDTO>(tablero);

            // Poblar nombre_usuario y nombre_rol desde AspNetUsers y AspNetRoles
            tableroDTO.nombre_usuario = await _userService.GetUserNameByIdAsync(tableroDTO.creado_por);
            tableroDTO.nombre_rol = await _roleService.GetRoleNameByIdAsync(tableroDTO.id_rol);

            return tableroDTO;
        }
    }
}

