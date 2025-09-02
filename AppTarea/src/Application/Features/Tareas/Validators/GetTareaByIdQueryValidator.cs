using FluentValidation;
using Application.Features.Tareas.Queries;
using Domain.Interfaces;

namespace Application.Features.Tareas.Validator
{
    public class GetTareaByIdQueryValidator : AbstractValidator<GetTareaByIdQuery>
    {
        private readonly ITareaRepository _tareaRepository;

        public GetTareaByIdQueryValidator(ITareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;

            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El ID de la Tarea debe ser mayor a 0")
            .MustAsync(TareaExistente).WithMessage("La Tarea no existe");
        }

        private async Task<bool> TareaExistente(int id, CancellationToken cancellationToken)
        {
            var tarea = await _tareaRepository.GetByIdAsync(id);
            return tarea != null;
        }

    }
}