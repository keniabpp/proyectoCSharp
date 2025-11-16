using Application.Features.Tableros.Commands;
using Application.Features.Tableros.DTOs;
using Application.Features.Columnas.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Features.Tableros.Handlers
{
    public class CreateTableroHandler : IRequestHandler<CreateTableroCommand, TableroDTO>
    {
        private readonly ITableroRepository _tableroRepository;
        private readonly IApplicationUserService _userService;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public CreateTableroHandler(ITableroRepository tableroRepository, IApplicationUserService userService, IMapper mapper, IRoleService roleService)
        {
            _tableroRepository = tableroRepository;
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
        }
        public async Task<TableroDTO> Handle(CreateTableroCommand request, CancellationToken cancellationToken)
        {
            var tablero = _mapper.Map<Tablero>(request.TableroCreateDTO);
            var tableroCreado = await _tableroRepository.CreateAsync(tablero);

            var tableroDTO = _mapper.Map<TableroDTO>(tableroCreado);
            
            // Poblar nombre_usuario y nombre_rol desde AspNetUsers y AspNetRoles
            tableroDTO.nombre_usuario = await _userService.GetUserNameByIdAsync(tableroDTO.creado_por);
            tableroDTO.nombre_rol = await _roleService.GetRoleNameByIdAsync(tableroDTO.id_rol);

            return tableroDTO;
        }  
    }
}