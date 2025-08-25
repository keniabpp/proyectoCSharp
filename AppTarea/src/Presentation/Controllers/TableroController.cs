using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Features.Tableros.Commands;
using Application.Features.Tableros.Queries;
using Application.Features.Tableros.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/tableros")]
    public class TableroController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TableroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/tableros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tablero>>> GetAll()
        {
            var query = new GetAllTablerosQuery();
            var tableros = await _mediator.Send(query);
            return Ok(tableros);
        }

        //GET: api/tableros/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Tablero>> GetById(int id)
        {
            var query = new GetTableroByIdQuery(id);
            var tablero = await _mediator.Send(query);

            if (tablero == null) return NotFound();
            return Ok(tablero);
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<Tablero>> Create([FromBody] TableroCreateDTO tableroCreateDTO)
        {
            var command = new CreateTableroCommand(tableroCreateDTO);
            var nuevoTablero = await _mediator.Send(command);

            if (nuevoTablero == null)
            {
                return NotFound(new { mensaje = "No se pudo crear el tablero." });
            }


            return CreatedAtAction(nameof(GetById), new { id = nuevoTablero.id_tablero }, nuevoTablero);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Tablero>> Update(int id, [FromBody] TableroUpdateDTO tableroUpdateDTO)
        {
            var command = new UpdateTableroCommand(id, tableroUpdateDTO);
            var actualizado = await _mediator.Send(command);

            if (actualizado == null) return NotFound();
            return Ok(new { mensaje = "Tablero actualizado correctamente" });
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteTableroCommand(id);
            var eliminado = await _mediator.Send(command);

            if (!eliminado) return NotFound();
            return Ok(new { mensaje = "Tablero eliminado correctamente." });
        }
    }
}