using MediatR;

namespace Application.Features.Tableros.Commands
{
    public class DeleteTableroCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteTableroCommand(int id)
        {
            Id = id;
        }
    }
}