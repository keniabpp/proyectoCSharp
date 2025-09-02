using FluentValidation;
using Application.Features.Tareas.Commands;
using Domain.Interfaces;

public class DeleteTareaCommandValidator : AbstractValidator<DeleteTareaCommand>
{
    private readonly ITareaRepository _tareaRepository;

    public DeleteTareaCommandValidator(ITareaRepository tareaRepository)
    {
        _tareaRepository = tareaRepository;

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
