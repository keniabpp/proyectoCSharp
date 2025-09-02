using FluentValidation;
using Application.Features.Tableros.Commands;

namespace Application.Features.Tableros.Validator
{
    public class CreateTableroCommandValidator : AbstractValidator<CreateTableroCommand>
    {
        public CreateTableroCommandValidator()
        {
            RuleFor(x => x.TableroCreateDTO.nombre)
            .NotEmpty().WithMessage("El nombre del tablero es obligatorio");
            

           
        }
    }
}