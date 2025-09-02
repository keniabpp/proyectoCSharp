using FluentValidation;
using Application.Features.Tableros.Commands;
using Domain.Interfaces;

namespace Application.Features.Tableros.Validator
{
    public class UpdateTableroCommandValidator : AbstractValidator<UpdateTableroCommand>
    {
        private readonly ITableroRepository _tableroRepository;

        public UpdateTableroCommandValidator(ITableroRepository tableroRepository)
        {
            _tableroRepository = tableroRepository;

            RuleFor(x => x.Id)
            .MustAsync(TableroExistente).WithMessage("Tablero no encontrado");

        }
        
        private async Task<bool> TableroExistente(int id, CancellationToken cancellationToken)
        {
            var tablero = await _tableroRepository.GetByIdAsync(id);
            return tablero != null;
        }

    }
}