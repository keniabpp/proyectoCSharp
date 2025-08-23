using Application.Features.Columnas.Commands;
using Application.Features.Columnas.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Columnas.Handlers
{
    public class CreateColumnaHandler : IRequestHandler<CreateColumnaCommand, ColumnaDTO>
    {
        private readonly IColumnaRepository _columnaRepository;
        private readonly IMapper _mapper;

        public CreateColumnaHandler(IColumnaRepository columnaRepository, IMapper mapper)
        {
            _columnaRepository = columnaRepository;
            _mapper = mapper;
        }

        public async Task<ColumnaDTO> Handle(CreateColumnaCommand request, CancellationToken cancellationToken)
        {
            var columna = _mapper.Map<Columna>(request.ColumnaCreateDTO);
            var columnaCreada = await _columnaRepository.CreateAsync(columna);
            var columnaDTO = _mapper.Map<ColumnaDTO>(columnaCreada);

            return columnaDTO;
        }
    }
}