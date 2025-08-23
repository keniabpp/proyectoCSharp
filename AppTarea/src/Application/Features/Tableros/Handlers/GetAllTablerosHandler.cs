using Application.Features.Tableros.DTOs;
using Application.Features.Tableros.Queries;
using Domain.Interfaces;
using MediatR;
using AutoMapper;

namespace Application.Features.Tableros.Handlers
{
    public class GetAllTablerosHandler : IRequestHandler<GetAllTablerosQuery, IEnumerable<TableroDTO>>
    {
        private readonly ITableroRepository _tableroRepository;
        private readonly IMapper _mapper;

        public GetAllTablerosHandler(ITableroRepository tableroRepository, IMapper mapper)
        {
            _tableroRepository = tableroRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TableroDTO>> Handle(GetAllTablerosQuery request, CancellationToken cancellationToken)
        {
            var tableros = await _tableroRepository.GetAllAsync();

            var tableroDTO = _mapper.Map<IEnumerable<TableroDTO>>(tableros);

            return tableroDTO;
        }
    }
}
