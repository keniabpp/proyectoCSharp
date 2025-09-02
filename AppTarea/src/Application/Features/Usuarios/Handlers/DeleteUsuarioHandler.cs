using Application.Features.Usuarios.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Usuarios.Handlers
{
    public class DeleteUsuarioHandler : IRequestHandler<DeleteUsuarioCommand, bool>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public DeleteUsuarioHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            return await _usuarioRepository.DeleteAsync(request.Id);
        }
    }
}
