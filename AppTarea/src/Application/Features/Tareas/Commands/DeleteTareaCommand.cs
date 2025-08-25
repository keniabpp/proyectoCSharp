using MediatR;

namespace Application.Features.Tareas.Commands
{
    public class DeleteTareaCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int creado_por { get; set; }

        public DeleteTareaCommand(int id, int creado_por)
        {
            Id = id;
            this.creado_por = creado_por;
        }
    }
}