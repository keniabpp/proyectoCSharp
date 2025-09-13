using FluentValidation;
using Application.Features.Usuarios.Commands;
using Domain.Interfaces;

namespace Application.Features.Usuarios.Validator
{
    public class UpdateUsuarioCommandValidator : AbstractValidator<UpdateUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UpdateUsuarioCommandValidator(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;

            RuleFor(x => x.UsuarioUpdateDTO.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.UsuarioUpdateDTO.Apellido)
            .NotEmpty().WithMessage("El apellido es obligatorio");
            RuleFor(x => x.UsuarioUpdateDTO.Telefono)
            .NotEmpty().WithMessage("El telefono solo puede ser numeros");

            RuleFor(x => x.Id)
            .MustAsync(UsuarioExistente).WithMessage("usuario no encontrado");

            RuleFor(x => x.UsuarioUpdateDTO.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("Formato de email inválido")
            .MustAsync((command, email, cancellation) => EmailUnico(email, command.Id, cancellation))
            .WithMessage("El correo ya está registrado por otro usuario.");


            RuleFor(x => x.UsuarioUpdateDTO.Contrasena)
            .Must(contrasena => ValidarContrasena(contrasena)) 
            .WithMessage("La contraseña no puede estar vacía.");



        }



        private async Task<bool> UsuarioExistente(int id, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return usuario != null;
        }

        private async Task<bool> EmailUnico(string email, int id_usuario, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(email);
            return usuario == null || usuario.id_usuario == id_usuario;
        }

        private bool ValidarContrasena(string contrasena)
        {
    
           if (string.IsNullOrEmpty(contrasena))
           {
               return true;
            }

            
           return true; 
        }


    }
}