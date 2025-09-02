using FluentValidation;
using Application.Features.Tareas.Commands;
using Domain.Interfaces;

namespace Application.Features.Tareas.Validator
{
    public class CreateTareaCommandValidator : AbstractValidator<CreateTareaCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITableroRepository _tableroRepository;
        private readonly IColumnaRepository _columnaRepository;

        public CreateTareaCommandValidator(IUsuarioRepository usuarioRepository,ITableroRepository tableroRepository,
            IColumnaRepository columnaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _tableroRepository = tableroRepository;
            _columnaRepository = columnaRepository;

            // Validaciones básicas
            RuleFor(x => x.TareaCreateDTO.titulo)
            .NotEmpty().WithMessage("El título es obligatorio");

            RuleFor(x => x.TareaCreateDTO.descripcion)
            .NotEmpty().WithMessage("La descripción es obligatoria");

            RuleFor(x => x.TareaCreateDTO.creado_por)
            .GreaterThan(0).WithMessage("El ID del creador debe ser mayor a 0")
            .MustAsync(UsuarioExistente).WithMessage("El usuario creador no existe");

            RuleFor(x => x.TareaCreateDTO.asignado_a)
            .GreaterThan(0).WithMessage("El ID del asignado debe ser mayor a 0")
            .MustAsync(UsuarioExistente).WithMessage("El usuario asignado no existe");

            RuleFor(x => x.TareaCreateDTO.id_tablero)
            .GreaterThan(0).WithMessage("El ID del tablero debe ser mayor a 0")
            .MustAsync(TableroExistente).WithMessage("El tablero no existe");

            RuleFor(x => x.TareaCreateDTO.id_columna)
            .GreaterThan(0).WithMessage("El ID de la columna debe ser mayor a 0")
            .MustAsync(ColumnaExistente).WithMessage("La columna no existe");
        }

        private async Task<bool> UsuarioExistente(int id, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return usuario != null;
        }

        private async Task<bool> TableroExistente(int id, CancellationToken cancellationToken)
        {
            var tablero = await _tableroRepository.GetByIdAsync(id);
            return tablero != null;
        }

        private async Task<bool> ColumnaExistente(int id, CancellationToken cancellationToken)
        {
            var columna = await _columnaRepository.GetByIdAsync(id);
            return columna != null;
        }
    }
}
