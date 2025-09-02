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
            return Ok(usuario);
        }

        // POST: api/usuarios
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<UsuarioDTO>> Create([FromBody] UsuarioCreateDTO usuarioCreateDTO)
        {
            var command = new CreateUsuarioCommand(usuarioCreateDTO);
            var nuevoUsuario = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = nuevoUsuario.id_usuario }, nuevoUsuario);
        }

        // PUT: api/usuarios/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioDTO>> Update(int id, [FromBody] UsuarioUpdateDTO usuarioUpdateDTO)
        {
            var command = new UpdateUsuarioCommand(id, usuarioUpdateDTO);
            var actualizado = await _mediator.Send(command);
            return Ok(new { mensaje = "Usuario actualizado correctamente" });
        }

        // DELETE: api/usuarios/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteUsuarioCommand(id);
            var eliminado = await _mediator.Send(command);

            return StatusCode(500, new { mensaje = "Usuario Eliminado Correctamente"});
        }


    }
}
