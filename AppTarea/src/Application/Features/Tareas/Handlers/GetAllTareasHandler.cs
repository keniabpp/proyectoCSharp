using Application.Features.Tareas.DTOs;
using Application.Features.Tareas.Queries;
using Domain.Interfaces;
using MediatR;
using AutoMapper;
using Application.Interfaces;

namespace Application.Features.Tareas.Handlers
{
    public class GetAllTareasHandler : IRequestHandler<GetAllTareasQuery, IEnumerable<TareaDTO>>
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationUserService _usuarioService;

        public GetAllTareasHandler(ITareaRepository tareaRepository, IMapper mapper, IApplicationUserService usuarioService)
        {
            _tareaRepository = tareaRepository;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        public async  Task<IEnumerable<TareaDTO>> Handle(GetAllTareasQuery request, CancellationToken cancellationToken)
        {
            var tareas = await _tareaRepository.GetAllAsync();

            var tareaDTO = _mapper.Map<IEnumerable<TareaDTO>>(tareas).ToList();

            // Poblar nombre_creador y nombre_asignado
            foreach (var dto in tareaDTO)
            {
                dto.nombre_creador = await _usuarioService.GetUserNameByIdAsync(dto.creado_por) ?? string.Empty;
                dto.nombre_asignado = await _usuarioService.GetUserNameByIdAsync(dto.asignado_a) ?? string.Empty;
            }

            return tareaDTO;
        }
    }
}