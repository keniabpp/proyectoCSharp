using Application.Features.Tareas.Commands;
using Application.Features.Tareas.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Tareas.Handlers
{
    public class CreateTareaHandler : IRequestHandler<CreateTareaCommand, TareaDTO>
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IColumnaRepository _columnaRepository;
        private readonly ITableroRepository _tableroRepository;

        public CreateTareaHandler(ITareaRepository tareaRepository,IMapper mapper,IUsuarioRepository usuarioRepository,
        IColumnaRepository columnaRepository,
        ITableroRepository tableroRepository)
        {
            _tareaRepository = tareaRepository;
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _columnaRepository = columnaRepository;
            _tableroRepository = tableroRepository;
        }

        public async Task<TareaDTO> Handle(CreateTareaCommand request, CancellationToken cancellationToken)
        {

            var usuarioCreador = await _usuarioRepository.GetByIdAsync(request.TareaCreateDTO.creado_por);
            if (usuarioCreador == null)
            {
                throw new Exception("Usuario creador no encontrado.");
            }
            
            var usuarioAsignado = await _usuarioRepository.GetByIdAsync(request.TareaCreateDTO.asignado_a);
            if (usuarioAsignado == null)
            {
                throw new Exception("Usuario asignado no encontrado.");
            }

            var tablero = await _tableroRepository.GetByIdAsync(request.TareaCreateDTO.id_tablero);
            if (tablero == null)
            {
                throw new Exception("Tablero no encontrado.");
            }

            var columna = await _columnaRepository.GetByIdAsync(request.TareaCreateDTO.id_columna);
            if (columna == null)
            {
                throw new Exception("Columna no encontrada.");
            }

            var tarea = _mapper.Map<Tarea>(request.TareaCreateDTO);
            tarea.creador = usuarioCreador; 
            tarea.asignado = usuarioAsignado; 
            tarea.tablero = tablero;  
            tarea.columna = columna;  
            

            var tareaCreada = await _tareaRepository.CreateAsync(tarea);
            var tareaDTO = _mapper.Map<TareaDTO>(tareaCreada);
            
            return tareaDTO;
        }
    }
}
