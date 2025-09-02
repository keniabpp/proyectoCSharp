using FluentValidation;
using Application.Features.Usuarios.Commands;

public class LoginUsuarioCommandValidator : AbstractValidator<LoginUsuarioCommand>
{
    public LoginUsuarioCommandValidator()
    {
        RuleFor(x => x.UsuarioLoginDTO.Email)
        .NotEmpty().WithMessage("El email es obligatorio");
        RuleFor(x => x.UsuarioLoginDTO.Contrasena)
        .NotEmpty().WithMessage("La contraseña es obligatoria");
    }
}
