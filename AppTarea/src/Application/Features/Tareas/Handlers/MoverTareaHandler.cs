using Application.Features.Tareas.Commands;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;
namespace Application.Features.Tareas.Handlers
{
    public class MoverTareaHandler : IRequestHandler<MoverTareaCommand, bool>
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IColumnaRepository _columnaRepository;

        public MoverTareaHandler(ITareaRepository tareaRepository, IColumnaRepository columnaRepository)
        {
            _tareaRepository = tareaRepository;
            _columnaRepository = columnaRepository;
        }

        public async Task<bool> Handle(MoverTareaCommand request, CancellationToken cancellationToken)
        {
            var tarea = await _tareaRepository.GetByIdAsync(request.MoverTareaDTO.id_tarea);
            if (tarea == null)return false;

            if (tarea.asignado_a != request.asignado_a)return false;

            var columnaActual = await _columnaRepository.GetByIdAsync(tarea.id_columna);
            if (columnaActual == null) return false;

            if (columnaActual.posicion == EstadoColumna.Hecho)
            {
                throw new Exception("La tarea ya está en la columna 'Hecho' y no se puede mover.");
            }
            if (tarea.fecha_vencimiento < DateTime.Now)
            {
                throw new Exception("no se puede mover la tarea por que ya caduco");
            } 

            // Verificar si la columna de destino existe
            var columna = await _columnaRepository.GetByIdAsync(request.MoverTareaDTO.id_columna);
            if (columna == null)
            {
                throw new Exception("La columna de destino no existe.");
               
            }

            tarea.detalle = request.MoverTareaDTO.detalle;

            return await _tareaRepository.MoverTareaAsync(request.MoverTareaDTO.id_tarea,
            request.MoverTareaDTO.id_columna);
        }
    }
}