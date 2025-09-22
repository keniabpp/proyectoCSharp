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
            RuleFor(x => x.TableroUpdateDTO.nombre)
            .NotEmpty().WithMessage("El nombre del tablero es obligatorio");

        }
        
        private async Task<bool> TableroExistente(int id, CancellationToken cancellationToken)
        {
            var tablero = await _tableroRepository.GetByIdAsync(id);
            return tablero != null;
        }

    }
}