using Application.Features.Tableros.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Tableros.Handlers
{
    public class DeleteTableroHandler : IRequestHandler<DeleteTableroCommand, bool>
    {
        private readonly ITableroRepository _tableroRepository;

        public DeleteTableroHandler(ITableroRepository tableroRepository)
        {
            _tableroRepository = tableroRepository;
        }

        public async Task<bool> Handle(DeleteTableroCommand request, CancellationToken cancellationToken)
        {
            var eliminado = await _tableroRepository.DeleteAsync(request.Id);

            if (!eliminado)
                throw new KeyNotFoundException($"No se encontró el tablero con ID {request.Id}");
             
             return true;
        }
    }
}