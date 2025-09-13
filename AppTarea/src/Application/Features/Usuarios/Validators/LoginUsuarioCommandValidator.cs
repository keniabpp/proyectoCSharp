using FluentValidation;
using Application.Features.Usuarios.Commands;

public class LoginUsuarioCommandValidator : AbstractValidator<LoginUsuarioCommand>
{
    public LoginUsuarioCommandValidator()
    {
        RuleFor(x => x.UsuarioLoginDTO.Email)
        .NotEmpty().WithMessage("El email es obligatorio");
        RuleFor(x => x.UsuarioLoginDTO.Contrasena)
        .NotEmpty().WithMessage("La contraseña es obligatoria")
        .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres")
        .Matches(@"[A-Z]+").WithMessage("La contraseña debe contener al menos una letra mayúscula")
        .Matches(@"[a-z]+").WithMessage("La contraseña debe contener al menos una letra minúscula")
        .Matches(@"[0-9]+").WithMessage("La contraseña debe contener al menos un número")
        .Matches(@"[!@#$%^&*()_+\-=\[\]{};:'"",<.>/?\\|]+")
        .WithMessage("La contraseña debe contener al menos un carácter especial");
    }
}
