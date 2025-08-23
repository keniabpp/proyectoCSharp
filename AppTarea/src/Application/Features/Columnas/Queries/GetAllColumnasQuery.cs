
using Application.Features.Columnas.DTOs;
using MediatR;

namespace Application.Features.Columnas.Queries
{
    public class GetAllColumnasQuery : IRequest<IEnumerable<ColumnaDTO>>
    {

    }
}