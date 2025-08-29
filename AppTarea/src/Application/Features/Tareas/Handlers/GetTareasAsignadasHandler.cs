using Application.Features.Tareas.DTOs;
using Application.Features.Tareas.Queries;
using Domain.Interfaces;
using MediatR;
using AutoMapper;

namespace Application.Features.Tareas.Handlers
{
    public class GetTareasAsignadasHandler : IRequestHandler<GetTareasAsignadasQuery, List<TareaDTO>>
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IMapper _mapper;

        public GetTareasAsignadasHandler(ITareaRepository tareaRepository, IMapper mapper)
        {
          _tareaRepository = tareaRepository;
          _mapper = mapper;
        }

        public async Task<List<TareaDTO>> Handle(GetTareasAsignadasQuery request, CancellationToken cancellationToken)
        {
            var tareas = await _tareaRepository.TareasAsignadasAsync(request.asignado_a);
            return _mapper.Map<List<TareaDTO>>(tareas);
        }
    }
}