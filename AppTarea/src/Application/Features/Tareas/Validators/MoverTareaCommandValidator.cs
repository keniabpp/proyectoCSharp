using FluentValidation;
using Application.Features.Tareas.Commands;
using Domain.Interfaces;

namespace Application.Features.Tareas.Validator
{
    public class MoverTareaCommandValidator : AbstractValidator<MoverTareaCommand>
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IColumnaRepository _columnaRepository;

        public MoverTareaCommandValidator(ITareaRepository tareaRepository, IColumnaRepository columnaRepository)
        {
            _tareaRepository = tareaRepository;
            _columnaRepository = columnaRepository;

            RuleFor(x => x.MoverTareaDTO.id_tarea)
            .GreaterThan(0).WithMessage("El ID de la tarea debe ser mayor a 0")
            .MustAsync(TareaExistente).WithMessage("La tarea no existe");

            RuleFor(x => x.MoverTareaDTO.id_columna)
            .GreaterThan(0).WithMessage("El ID de la columna de destino debe ser mayor a 0")
            .MustAsync(ColumnaExistente).WithMessage("La columna de destino no existe");
        }

        private async Task<bool> TareaExistente(int id_tarea, CancellationToken cancellationToken)
        {
            var tarea = await _tareaRepository.GetByIdAsync(id_tarea);
            return tarea != null;
        }

        private async Task<bool> ColumnaExistente(int id_columna, CancellationToken cancellationToken)
        {
            var columna = await _columnaRepository.GetByIdAsync(id_columna);
            return columna != null;
        }
    }
}
