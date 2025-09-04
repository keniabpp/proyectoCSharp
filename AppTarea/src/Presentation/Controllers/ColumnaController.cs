using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Features.Columnas.Queries;
using MediatR;
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

            return Ok(columna);
        }

       

        
    }
}