
using Application.Features.Columnas.DTOs;
using MediatR;

namespace Application.Features.Columnas.Queries
{
    public class GetColumnaByIdQuery : IRequest<ColumnaDTO>
    {
        public int Id { get; set; }
        public GetColumnaByIdQuery(int id)
        {
            Id = id;
        }
    }
}