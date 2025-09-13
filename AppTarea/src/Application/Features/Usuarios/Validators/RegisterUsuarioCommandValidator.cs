using FluentValidation;
using Application.Features.Usuarios.Commands;
using Domain.Interfaces;

namespace Application.Features.Usuarios.Validator
{
    public class RegisterUsuarioCommandValidator : AbstractValidator<RegisterUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public RegisterUsuarioCommandValidator(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;

            RuleFor(x => x.UsuarioRegisterDTO.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.UsuarioRegisterDTO.Apellido)
            .NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.UsuarioRegisterDTO.Telefono)
            .NotEmpty().WithMessage("El telefono solo puede ser numeros");
            RuleFor(x => x.UsuarioRegisterDTO.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("Formato de email inválido")
            .MustAsync(EmailUnico).WithMessage("El correo ya está registrado");
            RuleFor(x => x.UsuarioRegisterDTO.Contrasena)
            .NotEmpty().WithMessage("La contraseña es obligatoria")
            .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres")
            .Matches(@"[A-Z]+").WithMessage("La contraseña debe contener al menos una letra mayúscula")
            .Matches(@"[a-z]+").WithMessage("La contraseña debe contener al menos una letra minúscula")
            .Matches(@"[0-9]+").WithMessage("La contraseña debe contener al menos un número")
            .Matches(@"[!@#$%^&*()_+\-=\[\]{};:'"",<.>/?\\|]+")
            .WithMessage("La contraseña debe contener al menos un carácter especial");
        }
        private async Task<bool> EmailUnico(string email, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(email);
            return usuario == null; // válido si no existe
        }


    }
}