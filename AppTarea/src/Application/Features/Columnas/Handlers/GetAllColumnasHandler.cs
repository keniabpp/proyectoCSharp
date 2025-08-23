using Application.Features.Columnas.DTOs;
using Application.Features.Columnas.Queries;
using Domain.Interfaces;
using MediatR;
using AutoMapper;

namespace Application.Features.Columnas.Handlers
{
    public class GetAllColumnasHandler : IRequestHandler<GetAllColumnasQuery, IEnumerable<ColumnaDTO>>
    {
        private readonly IColumnaRepository _columnaRepository;
        private readonly IMapper _mapper;

        public GetAllColumnasHandler(IColumnaRepository columnaRepository, IMapper mapper)
        {
            _columnaRepository = columnaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ColumnaDTO>> Handle(GetAllColumnasQuery request, CancellationToken cancellationToken)
        {
            var columnas = await _columnaRepository.GetAllAsync();
            var columnaDTO = _mapper.Map<IEnumerable<ColumnaDTO>>(columnas);

            return columnaDTO;

        }
    }

    
}