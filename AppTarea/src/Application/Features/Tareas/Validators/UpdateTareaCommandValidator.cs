using FluentValidation;
using Application.Features.Tareas.Commands;
using Domain.Interfaces;

namespace Application.Features.Tareas.Validator
{
    public class UpdateTareaCommandValidator : AbstractValidator<UpdateTareaCommand>
    {
        private readonly ITareaRepository _tareaRepository;

        public UpdateTareaCommandValidator(ITareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;

            // Validar que la tarea exista
            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El ID de la tarea debe ser mayor a 0")
            .MustAsync(TareaExistente).WithMessage("La tarea no existe");



        }
        private async Task<bool> TareaExistente(int id, CancellationToken cancellationToken)
        {
            var tarea = await _tareaRepository.GetByIdAsync(id);
            return tarea != null;
        }

        
    }
}
