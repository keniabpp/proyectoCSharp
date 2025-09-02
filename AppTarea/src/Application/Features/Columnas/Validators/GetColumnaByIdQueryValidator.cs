using FluentValidation;
using Application.Features.Columnas.Queries;
using Domain.Interfaces;

namespace Application.Features.Columnas.Validator
{
    public class GetColumnaByIdQueryValidator : AbstractValidator<GetColumnaByIdQuery>
    {
        private readonly IColumnaRepository _columnaRepository;

        public GetColumnaByIdQueryValidator(IColumnaRepository columnaRepository)
        {
            _columnaRepository = columnaRepository;

            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El ID de la Columna debe ser mayor a 0")
            .MustAsync(ColumnaExistente).WithMessage("La Columna no existe");
        }

        private async Task<bool> ColumnaExistente(int id, CancellationToken cancellationToken)
        {
            var columna = await _columnaRepository.GetByIdAsync(id);
            return columna != null;
        }

    }
}