using Application.Features.Tareas.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Tareas.Handlers
{

    public class DeleteTareaHandler : IRequestHandler<DeleteTareaCommand, bool>
    {
        private readonly ITareaRepository _tareaRepository;

        public DeleteTareaHandler(ITareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }

        public async Task<bool> Handle(DeleteTareaCommand request, CancellationToken cancellationToken)
        {
            var tarea = await _tareaRepository.GetByIdAsync(request.Id);
            if (tarea == null)
                throw new KeyNotFoundException($"No se encontró la tarea con ID {request.Id}");

            // 2. Validar que el usuario autenticado sea el creador
            if (tarea.creado_por != request.creado_por)
                return false;

            // 3. Eliminar la tarea
            var tareaEliminada = await _tareaRepository.DeleteAsync(request.Id);
            return tareaEliminada;
        }
    }
    

    
}