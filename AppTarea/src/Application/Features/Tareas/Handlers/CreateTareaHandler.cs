using Application.Features.Tareas.Commands;
using Application.Features.Tareas.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Application.Interfaces;

namespace Application.Features.Tareas.Handlers
{
    public class CreateTareaHandler : IRequestHandler<CreateTareaCommand, TareaDTO>
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationUserService _usuarioService;
        private readonly IColumnaRepository _columnaRepository;
        private readonly ITableroRepository _tableroRepository;

        public CreateTareaHandler(ITareaRepository tareaRepository,IMapper mapper,IApplicationUserService usuarioService,
        IColumnaRepository columnaRepository,
        ITableroRepository tableroRepository)
        {
            _tareaRepository = tareaRepository;
            _mapper = mapper;
            _usuarioService = usuarioService;
            _columnaRepository = columnaRepository;
            _tableroRepository = tableroRepository;
        }

        public async Task<TareaDTO> Handle(CreateTareaCommand request, CancellationToken cancellationToken)
        {
            var tablero = await _tableroRepository.GetByIdAsync(request.TareaCreateDTO.id_tablero);
            var columna = await _columnaRepository.GetByIdAsync(request.TareaCreateDTO.id_columna);

            var tarea = _mapper.Map<Tarea>(request.TareaCreateDTO);
            // Los IDs ya vienen en el DTO, las propiedades de navegación se cargan automáticamente
            tarea.tablero = tablero!;  
            tarea.columna = columna!;  

            var tareaCreada = await _tareaRepository.CreateAsync(tarea);
            var tareaDTO = _mapper.Map<TareaDTO>(tareaCreada);
            
            // Poblar nombre_creador y nombre_asignado
            tareaDTO.nombre_creador = await _usuarioService.GetUserNameByIdAsync(tareaDTO.creado_por) ?? string.Empty;
            tareaDTO.nombre_asignado = await _usuarioService.GetUserNameByIdAsync(tareaDTO.asignado_a) ?? string.Empty;
            
            return tareaDTO;
        }
    }
}
