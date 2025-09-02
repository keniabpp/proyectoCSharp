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
            

            return await _tableroRepository.DeleteAsync(request.Id);
        }
    }
}