using Application.Features.Columnas.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Columnas.Handlers
{
    public class DeleteColumnaHandler : IRequestHandler<DeleteColumnaCommand, bool>
    {
        private readonly IColumnaRepository _columnaRepository;

        public DeleteColumnaHandler(IColumnaRepository columnaRepository)
        {
            _columnaRepository = columnaRepository;
            
        }

        public async Task<bool> Handle(DeleteColumnaCommand request, CancellationToken cancellationToken)
        {
            var columna = await _columnaRepository.DeleteAsync(request.Id);

            if (!columna)
                throw new KeyNotFoundException($"No se encontró la Columna con ID {request.Id}");
             
             return true;
        }
    }

}