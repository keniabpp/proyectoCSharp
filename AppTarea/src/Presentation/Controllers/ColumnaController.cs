using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Features.Columnas.Queries;
using Application.Features.Tableros.DTOs;
using MediatR;
using Application.Features.Columnas.Commands;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/columnas")]

    public class ColumnasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ColumnasController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET: api/columna
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Columna>>> GetAll()
        {
            var query = new GetAllColumnasQuery();
            var columnas = await _mediator.Send(query);
            return Ok(columnas);
        }


        //GET: api/columnas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Columna>> GetById(int id)
        {
            var query = new GetColumnaByIdQuery(id);
            var columna = await _mediator.Send(query);

            if (columna == null) return NotFound();
            return Ok(columna);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteColumnaCommand(id);
            var eliminado = await _mediator.Send(command);

            if (!eliminado) return NotFound();
            return Ok(new { mensaje = "Columna eliminado correctamente." });
        }

        
    }
}