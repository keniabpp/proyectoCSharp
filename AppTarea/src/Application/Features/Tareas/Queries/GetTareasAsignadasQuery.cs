using Application.Features.Tareas.DTOs;
using MediatR;

namespace Application.Features.Tareas.Queries
{
    public class GetTareasAsignadasQuery : IRequest<List<TareaDTO>>
    {
        public int asignado_a { get; set; }

        public GetTareasAsignadasQuery(int asignado_a)
        {
            this.asignado_a = asignado_a;
        }

    }
}