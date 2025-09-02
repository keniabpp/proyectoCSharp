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

            RuleFor(x => x.UsuarioRegisterDTO.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("Formato de email inválido")
            .MustAsync(EmailUnico).WithMessage("El correo ya está registrado");
        }
        private async Task<bool> EmailUnico(string email, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(email);
            return usuario == null; // válido si no existe
        }

        
    }
}