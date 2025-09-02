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
            
            RuleFor(x => x.Id)
            .MustAsync(UsuarioExistente).WithMessage("usuario no encontrado");

        }


        
        private async Task<bool> UsuarioExistente(int id, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return usuario != null;
        }

    }
}