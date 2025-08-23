using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Features.Tareas.Commands;
using Application.Features.Tareas.Queries;
using Application.Features.Tareas.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;

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

            if (tarea == null) return NotFound();
            return Ok(tarea);
        }


        [HttpPost]
        public async Task<ActionResult<Tarea>> Create([FromBody] TareaCreateDTO tareaCreateDTO)
        {
            var command = new CreateTareaCommand(tareaCreateDTO);
            var nuevaTarea = await _mediator.Send(command);

            if (nuevaTarea == null)
            {
                return BadRequest(new { mensaje = "No se pudo crear la Tarea." });
            }


            return CreatedAtAction(nameof(GetById), new { id = nuevaTarea.id_tarea }, nuevaTarea);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Tarea>> Update(int id, [FromBody] TareaUpdateDTO tareaUpdateDTO)
        {
            var command = new UpdateTareaCommand(id, tareaUpdateDTO);
            var tareaActualizada = await _mediator.Send(command);

            if (tareaActualizada == null) return NotFound();
            return Ok(new { mensaje = "Tarea actualizado correctamente" });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            // Extraer el ID del usuario autenticado desde el token JWT
            var id_usuario_claim = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(id_usuario_claim))
            return Unauthorized("No se pudo identificar al usuario.");

            var id_usuario = int.Parse(id_usuario_claim);

            // Crear el comando con el ID de la tarea y el ID del usuario
            var command = new DeleteTareaCommand(id, id_usuario);
            var eliminado = await _mediator.Send(command);

            if (!eliminado)
            return BadRequest("Solo el creador puede eliminar esta tarea.");

            return Ok(new { mensaje = "Tarea eliminada correctamente." });
        }
        

        [HttpPut("{id_tarea}/mover")]
        [Authorize]
        public async Task<IActionResult> MoverTarea([FromBody] MoverTareaDTO moverTareaDTO)
        {
           // Extraer el ID del usuario autenticado desde el token
           var id_usuario_claim = User.FindFirst("id")?.Value;
           if (string.IsNullOrEmpty(id_usuario_claim))
           return Unauthorized("No se pudo identificar al usuario.");

           int asignado_a = int.Parse(id_usuario_claim);

           try
           {
               // Crear y enviar el comando
               var command = new MoverTareaCommand(moverTareaDTO, asignado_a);
               var resultado = await _mediator.Send(command);

               if (!resultado)
               return BadRequest("Solo el usuario asignado puede mover esta tarea.");

               return Ok(new { mensaje = "Tarea movida correctamente." });
            }
            catch (InvalidOperationException ex)
            {
              // Si la tarea ha vencido, capturamos la InvalidOperationException lanzada en el handler
              return BadRequest(new { mensaje = ex.Message });
            }
            
        }


        
    }
}

