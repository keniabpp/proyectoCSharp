
using Moq;
using Domain.Entities;
using Domain.Interfaces;
using Application.Features.Usuarios.Commands;
using Application.Features.Usuarios.Queries;
using Application.Features.Usuarios.DTOs;
using Application.Features.Usuarios.Handlers;
using AutoMapper;

public class UsuarioHandlersTests
{
    [Fact]
    public async Task RegisterHandlerLanzaExcepcionSiEmailYaExiste()
    {
        // Arrange
        var dto = new UsuarioRegisterDTO
        {
            nombre = "Carlos",
            apellido = "Pérez",
            telefono = "987",
            email = "carlos@example.com",
            contrasena = "password"
        };

        // Crear un usuario existente con el mismo email
        var usuarioExistente = new Usuario
        {
            nombre = "Carlos",
            apellido = "Pérez",
            telefono = "987",
            email = dto.email, // Mismo email
            contrasena = "hashed_password",
            id_rol = 2 // Usuario normal
        };

        var repo = new Mock<IUsuarioRepository>();
        // El método GetByEmailAsync retorna un usuario existente con el mismo email
        repo.Setup(r => r.GetByEmailAsync(dto.email)).ReturnsAsync(usuarioExistente);

        var mapper = new Mock<IMapper>();

        var handler = new RegisterUsuarioCommandHandler(repo.Object, mapper.Object);

        // Act & Assert
        // Verificamos que se lanza la excepción de InvalidOperationException si el correo ya está registrado
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
          handler.Handle(new RegisterUsuarioCommand(dto), default));
    }


    [Fact]
    public async Task CreateHandlerLanzaExcepcionSiEmailYaExiste()
    {
        // Arrange
        var dto = new UsuarioCreateDTO
        {
            nombre = "Carlos",
            apellido = "Pérez",
            telefono = "987",
            email = "carlos@example.com",
            contrasena = "password",
            id_rol = 1 // Rol admin
        };

        // Crear un usuario existente con el mismo email
        var usuarioExistente = new Usuario
        {
            nombre = "Carlos",
            apellido = "Pérez",
            telefono = "987",
            email = dto.email,
            contrasena = "hashed_password",
            id_rol = 1 // Admin
        };

        var repo = new Mock<IUsuarioRepository>();
        // El método GetByEmailAsync retorna un usuario existente con el mismo email
        repo.Setup(r => r.GetByEmailAsync(dto.email)).ReturnsAsync(usuarioExistente);

        var mapper = new Mock<IMapper>();

        var handler = new CreateUsuarioHandler(repo.Object, mapper.Object);

        // Act & Assert
        // Verificamos que se lanza la excepción de InvalidOperationException si el correo ya está registrado
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
        handler.Handle(new CreateUsuarioCommand(dto), default));
    }








    [Fact]
    public async Task GetByIdHandlerLanzaExcepcionSiNoExiste()
    {
        // Arrange
        var repo = new Mock<IUsuarioRepository>();

        // Configura el mock para lanzar una excepción si no encuentra el usuario
        repo.Setup(r => r.GetByIdAsync(999)).ThrowsAsync(new KeyNotFoundException("Usuario no encontrado."));

        var mapper = new Mock<IMapper>();
        var handler = new GetUsuarioByIdHandler(repo.Object, mapper.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
          await handler.Handle(new GetUsuarioByIdQuery(999), default));

        // Assert
        Assert.Equal("Usuario no encontrado.", exception.Message);
    }



    [Fact]
    public async Task UpdateHandlerLanzaExcepcionSiEmailYaExiste()
    {
        // Arrange
        var usuarioExistente = new Usuario { id_usuario = 1, email = "ana@example.com" };
        var dto = new UsuarioUpdateDTO { email = "otro@example.com" };

        // Mock del repositorio
        var repo = new Mock<IUsuarioRepository>();
        repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(usuarioExistente);  // El usuario con ID 1
        repo.Setup(r => r.GetByEmailAsync("otro@example.com")).ReturnsAsync(new Usuario { id_usuario = 2, email = "otro@example.com" });  // Email duplicado

        var mapper = new Mock<IMapper>();
        var handler = new UpdateUsuarioHandler(repo.Object, mapper.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            handler.Handle(new UpdateUsuarioCommand(1, dto), default));

        Assert.Equal("El correo electrónico ya está en uso por otro usuario.", exception.Message);
    }


    [Fact]
    public async Task DeleteHandler_EliminaUsuario_Exitosamente()
    {
       // Arrange
       var usuarioExistente = new Usuario
       {
          id_usuario = 1,
          nombre = "Carlos",
          apellido = "Pérez",
          telefono = "987",
          email = "carlos@example.com"
        };

       var repo = new Mock<IUsuarioRepository>();
       // Simulamos que el usuario existe
       repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(usuarioExistente);
       // Simulamos que la eliminación es exitosa
       repo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

      var handler = new DeleteUsuarioHandler(repo.Object);

       // Act
      var result = await handler.Handle(new DeleteUsuarioCommand(1), default);

      // Assert
      Assert.True(result);  // Verificamos que la eliminación fue exitosa
    }

}
