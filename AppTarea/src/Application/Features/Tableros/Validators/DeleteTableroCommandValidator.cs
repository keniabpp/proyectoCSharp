using FluentValidation;
using Domain.Interfaces;
using System.Data;
using Application.Features.Tableros.Commands;

namespace Application.Features.Tableros.Validator
{
    public class DeleteTableroCommandValidator : AbstractValidator<DeleteTableroCommand>
    {
        private readonly ITableroRepository _tableroRepository;

        public DeleteTableroCommandValidator(ITableroRepository tableroRepository)
        {
            _tableroRepository = tableroRepository;

            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El ID del Tablero debe ser mayor a 0")
            .MustAsync(TableroExistente).WithMessage("El Tablero no existe");
        }

        private async Task<bool> TableroExistente(int id, CancellationToken cancellationToken)
        {
            var tablero = await _tableroRepository.GetByIdAsync(id);
            return tablero != null;
        }


    }
}