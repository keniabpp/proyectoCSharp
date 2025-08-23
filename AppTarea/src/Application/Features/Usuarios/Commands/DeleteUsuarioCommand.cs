using MediatR;

namespace Application.Features.Usuarios.Commands
{
    public class DeleteUsuarioCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteUsuarioCommand(int id)
        {
            Id = id;
        }
    }
}
