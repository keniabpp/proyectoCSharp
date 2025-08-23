using Application.Features.Tareas.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tareas.Commands
{
    public class CreateTareaCommand : IRequest<TareaDTO>
    {
        public TareaCreateDTO TareaCreateDTO { get; set; }

        public CreateTareaCommand(TareaCreateDTO tareaCreateDTO)
        {
            TareaCreateDTO = tareaCreateDTO;
        }
    }
}