using Application.Features.Tareas.DTOs;
using Application.Features.Tareas.Queries;
using Domain.Interfaces;
using MediatR;
using AutoMapper;
using Application.Interfaces;

namespace Application.Features.Tareas.Handlers
{
    public class GetTareasAsignadasHandler : IRequestHandler<GetTareasAsignadasQuery, List<TareaDTO>>
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationUserService _usuarioService;

        public GetTareasAsignadasHandler(ITareaRepository tareaRepository, IMapper mapper, IApplicationUserService usuarioService)
        {
          _tareaRepository = tareaRepository;
          _mapper = mapper;
          _usuarioService = usuarioService;
        }

        public async Task<List<TareaDTO>> Handle(GetTareasAsignadasQuery request, CancellationToken cancellationToken)
        {
            var tareas = await _tareaRepository.TareasAsignadasAsync(request.asignado_a);
            var tareasDTO = _mapper.Map<List<TareaDTO>>(tareas);
            
            // Poblar nombre_creador y nombre_asignado
            foreach (var dto in tareasDTO)
            {
                dto.nombre_creador = await _usuarioService.GetUserNameByIdAsync(dto.creado_por) ?? string.Empty;
                dto.nombre_asignado = await _usuarioService.GetUserNameByIdAsync(dto.asignado_a) ?? string.Empty;
            }
            
            return tareasDTO;
        }
    }
}