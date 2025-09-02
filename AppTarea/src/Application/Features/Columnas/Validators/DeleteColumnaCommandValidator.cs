using FluentValidation;
using Domain.Interfaces;
using Application.Features.Columnas.Commands;


namespace Application.Features.Columnas.Validator
{
    public class DeleteColumnaCommandValidator : AbstractValidator<DeleteColumnaCommand>
    {
        private readonly IColumnaRepository _columnaRepository;

        public DeleteColumnaCommandValidator(IColumnaRepository columnaRepository)
        {
            _columnaRepository = columnaRepository;

            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El ID de la Columna debe ser mayor a 0")
            .MustAsync(ColumnaExistente).WithMessage("La columna no existe");
        }

        private async Task<bool> ColumnaExistente(int id, CancellationToken cancellationToken)
        {
            var columna = await _columnaRepository.GetByIdAsync(id);
            return columna != null;
        }


    }
}