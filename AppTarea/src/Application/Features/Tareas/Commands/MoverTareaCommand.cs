using Application.Features.Tareas.DTOs;
using MediatR;

namespace Application.Features.Tareas.Commands
{
    public class MoverTareaCommand : IRequest<TareaDTO>
    {
        public MoverTareaDTO MoverTareaDTO { get; set; }
        public int asignado_a { get; set; }

        public MoverTareaCommand(MoverTareaDTO moverTareaDTO, int asignado_a)
        {
            MoverTareaDTO = moverTareaDTO;
            this.asignado_a = asignado_a;
        }
    }
}
