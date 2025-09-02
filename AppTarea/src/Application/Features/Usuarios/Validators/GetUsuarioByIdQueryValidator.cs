using FluentValidation;
using Application.Features.Usuarios.Queries;
using Domain.Interfaces;

namespace Application.Features.Usuarios.Validator
{
    public class GetUsuarioByIdQueryValidator : AbstractValidator<GetUsuarioByIdQuery>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public GetUsuarioByIdQueryValidator(IUsuarioRepository usuarioRepository)
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