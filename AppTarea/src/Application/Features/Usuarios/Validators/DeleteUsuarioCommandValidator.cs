using FluentValidation;
using Application.Features.Usuarios.Commands;
using Domain.Interfaces;
using System.Data;

namespace Application.Features.Usuarios.Validator
{
    public class DeleteUsuarioCommandValidator : AbstractValidator<DeleteUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public DeleteUsuarioCommandValidator(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;

            RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El ID del usuario debe ser mayor a 0")
            .MustAsync(UsuarioExistente).WithMessage("El Usuario no existe");
        }

        private async Task<bool> UsuarioExistente(int id, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return usuario != null;
        }


    }
}