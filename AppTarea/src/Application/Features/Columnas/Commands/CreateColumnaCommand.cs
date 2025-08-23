using Application.Features.Columnas.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Features.Columnas.Commands
{
    public class CreateColumnaCommand : IRequest<ColumnaDTO>
    {
        public ColumnaCreateDTO ColumnaCreateDTO { get; set; }

        public CreateColumnaCommand(ColumnaCreateDTO columnaCreateDTO)
        {
            ColumnaCreateDTO = columnaCreateDTO;
        }
        
    }
}