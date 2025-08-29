using Application.Features.Tareas.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tareas.Commands
{
    public class UpdateTareaCommand : IRequest<TareaDTO>
    {
        public int Id { get; set; }
        public int creado_por { get; set; }

        public TareaUpdateDTO TareaUpdateDTO { get; set; }

        public UpdateTareaCommand(int id, TareaUpdateDTO tareaUpdateDTO, int creado_por)
        {
            Id = id;
            this.creado_por = creado_por;
            TareaUpdateDTO = tareaUpdateDTO;
        }
    }
}