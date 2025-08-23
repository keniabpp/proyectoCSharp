
using Application.Features.Tareas.DTOs;
using MediatR;

namespace Application.Features.Tareas.Queries
{
    public class GetTareaByIdQuery : IRequest<TareaDTO>
    {
        public int Id { get; set; }
        public GetTareaByIdQuery(int id)
        {
            Id = id;
        }
    }
}