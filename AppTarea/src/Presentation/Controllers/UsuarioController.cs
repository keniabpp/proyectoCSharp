using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Features.Usuarios.Commands;
using Application.Features.Usuarios.Queries;
using Application.Features.Usuarios.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("register")]
        public async Task<ActionResult<Usuario>> Register([FromBody] UsuarioRegisterDTO usuarioRegisterDTO)
        {
          var command = new RegisterUsuarioCommand(usuarioRegisterDTO);
          var nuevoUsuario = await _mediator.Send(command);

          if (nuevoUsuario == null)
          return BadRequest("No se pudo registrar el usuario");

          return CreatedAtAction(nameof(GetById), new { id = nuevoUsuario.id_rol }, nuevoUsuario);
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO loginDto)
        {
          var result = await _mediator.Send(new LoginUsuarioCommand(loginDto));

          if (result == null)
          return Unauthorized(new { message = "Credenciales inválidas" });

          return Ok(result);
        }



        // GET: api/usuarios
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            var query = new GetAllUsuariosQuery();
            var usuarios = await _mediator.Send(query);
            return Ok(usuarios);
        }

        // GET: api/usuarios/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var query = new GetUsuarioByIdQuery(id);
            var usuario = await _mediator.Send(query);

            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        // POST: api/usuarios
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Usuario>> Create([FromBody] UsuarioCreateDTO usuarioCreateDTO)
        {
            var command = new CreateUsuarioCommand(usuarioCreateDTO);
            var nuevoUsuario = await _mediator.Send(command);

            if (nuevoUsuario == null)
            {
                return BadRequest(new { mensaje = "No se pudo crear el usuario." });
            }

            return CreatedAtAction(nameof(GetById), new { id = nuevoUsuario.id_rol }, nuevoUsuario);
        }

        // PUT: api/usuarios/{id}
        [HttpPut("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<Usuario>> Update(int id, [FromBody] UsuarioUpdateDTO usuarioUpdateDTO)
        {
            var command = new UpdateUsuarioCommand(id, usuarioUpdateDTO);
            var actualizado = await _mediator.Send(command);

            if (actualizado == null) return NotFound();
            return Ok(new { mensaje = "Usuario actualizado correctamente" });
        }

        // DELETE: api/usuarios/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
           try
           {
               // Llamar al handler para eliminar el usuario
               var command = new DeleteUsuarioCommand(id);
               var eliminado = await _mediator.Send(command);

               // Si el usuario no fue eliminado, devolver un BadRequest con mensaje
               if (!eliminado)
               {
                 return BadRequest(new { mensaje = "No se encontró el usuario con el ID proporcionado." });
                }

               // Si se eliminó correctamente, devolver un Ok con mensaje
               return Ok(new { mensaje = "Usuario eliminado correctamente." });
            }
            catch (InvalidOperationException ex)
            {
               // Si ocurre una InvalidOperationException, devolver un BadRequest con el mensaje de error
               return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
               // Capturar cualquier otra excepción y devolver un BadRequest con el código 500
               return StatusCode(500, new { mensaje = "Hubo un error al eliminar el usuario.", detalle = ex.Message });
            }
        }


    }
}
