using Application.Features.Tareas.Commands;
using Application.Features.Tareas.DTOs;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;
using Application.Interfaces;

namespace Application.Features.Tareas.Handlers
{
    public class MoverTareaHandler : IRequestHandler<MoverTareaCommand, TareaDTO>
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IColumnaRepository _columnaRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationUserService _usuarioService;

        public MoverTareaHandler(ITareaRepository tareaRepository, IColumnaRepository columnaRepository, IMapper mapper, IApplicationUserService usuarioService)
        {
            _tareaRepository = tareaRepository;
            _columnaRepository = columnaRepository;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        public async Task<TareaDTO> Handle(MoverTareaCommand request, CancellationToken cancellationToken)
        {
            var tarea = await _tareaRepository.GetByIdAsync(request.MoverTareaDTO.id_tarea);
            if (tarea == null) throw new KeyNotFoundException("Tarea no encontrada.");

            if (tarea.asignado_a != request.asignado_a)
                throw new InvalidOperationException("Solo el Usuario asignado puede mover la Tarea");

            var columnaActual = await _columnaRepository.GetByIdAsync(tarea.id_columna);
            if (columnaActual == null) throw new KeyNotFoundException("Columna no encontrada.");

            if (columnaActual.posicion == EstadoColumna.Hecho)
            {
                throw new InvalidOperationException("La tarea ya está en la columna 'Hecho' y no se puede mover.");
            }
            if (tarea.fecha_vencimiento < DateTime.Now)
            {
                throw new InvalidOperationException("no se puede mover la tarea por que ya caduco");
            }

            tarea.detalle = request.MoverTareaDTO.detalle;

            await _tareaRepository.MoverTareaAsync(request.MoverTareaDTO.id_tarea, request.MoverTareaDTO.id_columna);

            
            var tareaActualizada = await _tareaRepository.GetByIdAsync(request.MoverTareaDTO.id_tarea);

            var tareaDTO = _mapper.Map<TareaDTO>(tareaActualizada);
            
            // Poblar nombre_creador y nombre_asignado
            tareaDTO.nombre_creador = await _usuarioService.GetUserNameByIdAsync(tareaDTO.creado_por) ?? string.Empty;
            tareaDTO.nombre_asignado = await _usuarioService.GetUserNameByIdAsync(tareaDTO.asignado_a) ?? string.Empty;
            
            return tareaDTO;
        }
        
        
    }
}