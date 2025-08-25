using Application.Features.Columnas.DTOs;
using Application.Features.Columnas.Queries;
using Domain.Interfaces;
using MediatR;
using AutoMapper;

namespace Application.Features.Columnas.Handlers
{
    public class GetColumnaByIdHandler : IRequestHandler<GetColumnaByIdQuery, ColumnaDTO>
    {
        private readonly IColumnaRepository _columnaRepository;
        private readonly IMapper _mapper;

        public GetColumnaByIdHandler(IColumnaRepository columnaRepository, IMapper mapper)
        {
            _columnaRepository = columnaRepository;
            _mapper = mapper;
        }

        public async Task<ColumnaDTO> Handle(GetColumnaByIdQuery request, CancellationToken cancellationToken)
        {
            var columna = await _columnaRepository.GetByIdAsync(request.Id);

            if (columna == null)
            {
                throw new Exception($"Columna con ID {request.Id} no encontrado.");
            }
                

            var columnaDTO = _mapper.Map<ColumnaDTO>(columna);

            return columnaDTO;
        }
    }
}