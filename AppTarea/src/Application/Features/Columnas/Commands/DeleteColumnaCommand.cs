using MediatR;


namespace Application.Features.Columnas.Commands
{
    public class DeleteColumnaCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteColumnaCommand(int id)
        {
            Id = id;
        }
    }
}