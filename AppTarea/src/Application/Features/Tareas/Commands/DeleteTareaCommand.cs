using MediatR;

namespace Application.Features.Tareas.Commands
{
    public class DeleteTareaCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int Id_usuario { get; set; }

        public DeleteTareaCommand(int id, int id_usuario)
        {
            Id = id;
            Id_usuario = id_usuario;
        }
    }
}