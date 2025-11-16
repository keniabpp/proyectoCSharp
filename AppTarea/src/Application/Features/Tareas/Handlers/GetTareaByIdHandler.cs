using Application.Features.Tareas.DTOs;
using Application.Features.Tareas.Queries;
using Domain.Interfaces;
using MediatR;
using AutoMapper;
using Application.Interfaces;

namespace Application.Features.Tareas.Handlers
{
    public class GetTareaByIdHandler : IRequestHandler<GetTareaByIdQuery, TareaDTO>
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationUserService _usuarioService;

        public GetTareaByIdHandler(ITareaRepository tareaRepository, IMapper mapper, IApplicationUserService usuarioService)
        {
            _tareaRepository = tareaRepository;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        public async Task<TareaDTO> Handle(GetTareaByIdQuery request, CancellationToken cancellationToken)
        {
            var tarea = await _tareaRepository.GetByIdAsync(request.Id);
            if (tarea == null)
                throw new KeyNotFoundException("Tarea no encontrada.");
            
            var tareaDTO = _mapper.Map<TareaDTO>(tarea);

            // Poblar nombre_creador y nombre_asignado
            tareaDTO.nombre_creador = await _usuarioService.GetUserNameByIdAsync(tareaDTO.creado_por) ?? string.Empty;
            tareaDTO.nombre_asignado = await _usuarioService.GetUserNameByIdAsync(tareaDTO.asignado_a) ?? string.Empty;

            return tareaDTO;
        }
    }
}

