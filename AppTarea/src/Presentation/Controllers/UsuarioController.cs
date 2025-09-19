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
            
            var response = new
    {
        nuevoUsuario.id_usuario,
        nuevoUsuario.Nombre,
        nuevoUsuario.Apellido,
        nuevoUsuario.Email,
        nuevoUsuario.Telefono,
        Rol = nuevoUsuario.rolNombre?.ToLower() // normalizamos a minúscula
    };

            return CreatedAtAction(nameof(GetById), new
            {
                id = nuevoUsuario.id_rol
            }, nuevoUsuario);
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO usuarioLoginDTO)
        {
            Console.WriteLine("Login ejecutado para: " + usuarioLoginDTO.Email);

            var result = await _mediator.Send(new LoginUsuarioCommand(usuarioLoginDTO));

            if (result == null)
                return Unauthorized(new { message = "Credenciales inválidas" });


            Response.Cookies.Append("token", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // Asegúrate de usar HTTPS en producción
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return Ok(new
            {
                result.id_usuario,
                result.Email,
                Rol = result.Rol.ToLower(),
                result.Nombre
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            return Ok(new { message = "Sesión cerrada correctamente" });
        }




        // GET: api/usuarios
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            var query = new GetAllUsuariosQuery();
            var usuarios = await _mediator.Send(query);
            return Ok(usuarios);
        }


        // GET: api/usuarios/{id}
        [HttpGet("{id}")]
        
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

            return Ok(new { mensaje = "Usuario eliminado correctamente" });
        }


    }
}
