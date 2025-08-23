using Application.Features.Tareas.DTOs;
using Application.Features.Tareas.Queries;
using Domain.Interfaces;
using MediatR;
using AutoMapper;

namespace Application.Features.Tareas.Handlers
{
    public class GetAllTareasHandler : IRequestHandler<GetAllTareasQuery, IEnumerable<TareaDTO>>
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IMapper _mapper;

        public GetAllTareasHandler(ITareaRepository tareaRepository, IMapper mapper)
        {
            _tareaRepository = tareaRepository;
            _mapper = mapper;
        }

        public async  Task<IEnumerable<TareaDTO>> Handle(GetAllTareasQuery request, CancellationToken cancellationToken)
        {
            var tareas = await _tareaRepository.GetAllAsync();

            var tareaDTO = _mapper.Map<IEnumerable<TareaDTO>>(tareas);

            return tareaDTO;
        }
    }
}