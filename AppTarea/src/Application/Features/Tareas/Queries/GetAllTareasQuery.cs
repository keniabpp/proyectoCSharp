using Application.Features.Tareas.DTOs;
using MediatR;

namespace Application.Features.Tareas.Queries
{
    public class GetAllTareasQuery : IRequest<IEnumerable<TareaDTO>>
    {

    }
}