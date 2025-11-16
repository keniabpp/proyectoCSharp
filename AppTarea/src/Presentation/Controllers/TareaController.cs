using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Features.Tareas.Commands;
using Application.Features.Tareas.Queries;
using Application.Features.Tareas.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/tareas")]

    public class TareaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TareaController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("tareasAsignadas")]
        public async Task<ActionResult<List<TareaDTO>>> GetTareasAsignadas(int id_usuario)
        {
            

            var query = new GetTareasAsignadasQuery(id_usuario);
            var tareas = await _mediator.Send(query);

            return Ok(tareas);
        }


        // GET: api/tareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetAll()
        {
            var query = new GetAllTareasQuery();
            var tareas = await _mediator.Send(query);
            return Ok(tareas);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> GetById(int id)
        {
            var query = new GetTareaByIdQuery(id);
            var tarea = await _mediator.Send(query);

            return Ok(tarea);
        }


        [HttpPost]
        public async Task<ActionResult<Tarea>> Create([FromBody] TareaCreateDTO tareaCreateDTO)
        {
            var command = new CreateTareaCommand(tareaCreateDTO);
            var nuevaTarea = await _mediator.Send(command);
            
            return CreatedAtAction(nameof(GetById), new { id = nuevaTarea.id_tarea }, nuevaTarea);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Tarea>> Update(int id, [FromBody] TareaUpdateDTO tareaUpdateDTO)
        {
            // Extraer el ID del usuario autenticado desde el token JWT
            var id_usuario_claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(id_usuario_claim))
            return Unauthorized("No se pudo identificar al usuario.");

            var id_usuario = int.Parse(id_usuario_claim);

            var command = new UpdateTareaCommand(id, tareaUpdateDTO, id_usuario);
            var tareaActualizada = await _mediator.Send(command);
            
            return Ok(new { mensaje = "Tarea actualizado correctamente" });
        }
        

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            // Extraer el ID del usuario autenticado desde el token JWT
            var id_usuario_claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(id_usuario_claim))
            return Unauthorized("No se pudo identificar al usuario.");

            var id_usuario = int.Parse(id_usuario_claim);

            // Crear el comando con el ID de la tarea y el ID del usuario
            var command = new DeleteTareaCommand(id, id_usuario);
            var eliminado = await _mediator.Send(command);

            return Ok(new { mensaje = "Tarea eliminada correctamente." });
        }
        

        [HttpPut("{id_tarea}/moverTarea")]
        [Authorize]
        public async Task<IActionResult> MoverTarea([FromBody] MoverTareaDTO moverTareaDTO)
        {
           // Extraer el ID del usuario autenticado desde el token
           var id_usuario_claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           if (string.IsNullOrEmpty(id_usuario_claim))
           return Unauthorized("No se pudo identificar al usuario.");

           int asignado_a = int.Parse(id_usuario_claim);

          
            var command = new MoverTareaCommand(moverTareaDTO, asignado_a);
            await _mediator.Send(command);

               
            return Ok(new { mensaje = "Tarea movida correctamente." });
        }
            
        


        
    }
}

