using Application.Features.Tareas.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tareas.Commands
{
    public class UpdateTareaCommand : IRequest<TareaDTO>
    {
        public int Id { get; set; }

        public TareaUpdateDTO TareaUpdateDTO { get; set; }

        public UpdateTareaCommand(int id, TareaUpdateDTO tareaUpdateDTO)
        {
            Id = id;
            TareaUpdateDTO = tareaUpdateDTO;
        }
    }
}