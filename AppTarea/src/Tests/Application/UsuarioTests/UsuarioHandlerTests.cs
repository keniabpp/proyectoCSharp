using Application.Features.Usuarios.Commands;
using Application.Features.Usuarios.DTOs;
using Application.Features.Usuarios.Handlers;
using Application.Features.Usuarios.Queries;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Xunit;

namespace Tests.Application.UsuarioTests;

public class UsuarioHandlerTests
{
    /// <summary>
    /// Verifica que RegisterUsuarioCommandHandler registre un usuario correctamente
    /// </summary>
    [Fact]
    public async Task RegisterAsync_Should_ReturnUsuarioDTO_When_UserIsRegisteredSuccessfully()
    {
        // Arrange
        var dto = new UsuarioRegisterDTO
        {
            Nombre = "Ana",
            Apellido = "Pérez",
            Telefono = "123456",
            Email = "ana@example.com",
            Contrasena = "password"
        };

        var usuarioCreado = new Usuario { id_rol = 2, nombre = dto.Nombre, email = dto.Email };

        var userServiceMock = new Mock<IApplicationUserService>();
        var serviceResult = (IsSuccess: true, User: usuarioCreado, Errors: Enumerable.Empty<string>());
        userServiceMock.Setup(r => r.CreateUserWithRoleIdAsync(
            dto.Email, dto.Contrasena, dto.Nombre, dto.Apellido, dto.Telefono, 2))
            .ReturnsAsync(serviceResult);

        var roleServiceMock = new Mock<IRoleService>();
        roleServiceMock.Setup(r => r.GetRoleNameByIdAsync(It.IsAny<int>())).ReturnsAsync("Usuario");

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<UsuarioRegisterResponseDTO>(usuarioCreado))
        .Returns(new UsuarioRegisterResponseDTO { id_usuario = "1", Nombre = usuarioCreado.nombre, Email = usuarioCreado.email, id_rol = usuarioCreado.id_rol });

        var handler = new RegisterUsuarioCommandHandler(userServiceMock.Object, mapperMock.Object, roleServiceMock.Object);

        // Act
        var result = await handler.Handle(new RegisterUsuarioCommand(dto), default);

        // Assert
        Assert.Equal(2, result.id_rol);
        Assert.Equal(dto.Nombre, result.Nombre);
        Assert.Equal(dto.Email, result.Email);
        userServiceMock.Verify(r => r.CreateUserWithRoleIdAsync(
            dto.Email, dto.Contrasena, dto.Nombre, dto.Apellido, dto.Telefono, 2), Times.Once);
    }

    /// <summary>
    /// Verifica que CreateUsuarioHandler cree un usuario con rol asignado por admin
    /// </summary>
    [Fact]
    public async Task CreateAsync_Should_ReturnUsuarioDTO_When_AdminCreatesUserWithRole()
    {
        // Arrange
        var dto = new UsuarioCreateDTO
        {
            Nombre = "Juan",
            Apellido = "Ramírez",
            Telefono = "654321",
            Email = "juan@example.com",
            Contrasena = "password",
            id_rol = 1 // Admin asigna el rol
        };

        var usuarioCreado = new Usuario { nombre = dto.Nombre, email = dto.Email, id_rol = dto.id_rol };

        // Mock del servicio de Identity
        var serviceResult = (IsSuccess: true, User: usuarioCreado, Errors: Enumerable.Empty<string>());
        var serviceMock = new Mock<IApplicationUserService>();
        serviceMock.Setup(s => s.CreateUserWithRoleIdAsync(
            dto.Email, dto.Contrasena, dto.Nombre, dto.Apellido, dto.Telefono, dto.id_rol))
            .ReturnsAsync(serviceResult);

        var roleServiceMock = new Mock<IRoleService>();
        roleServiceMock.Setup(r => r.GetRoleNameByIdAsync(It.IsAny<int>())).ReturnsAsync("Admin");

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<UsuarioDTO>(usuarioCreado)).Returns(
            new UsuarioDTO { id_usuario = "1", Nombre = usuarioCreado.nombre, Email = usuarioCreado.email, id_rol = usuarioCreado.id_rol });

        var handler = new CreateUsuarioHandler(serviceMock.Object, mapperMock.Object, roleServiceMock.Object);

        // Act
        var result = await handler.Handle(new CreateUsuarioCommand(dto), default);

        // Assert
        Assert.Equal(dto.id_rol, result.id_rol);
        Assert.Equal(dto.Nombre, result.Nombre);
        Assert.Equal(dto.Email, result.Email);
        serviceMock.Verify(s => s.CreateUserWithRoleIdAsync(
            dto.Email, dto.Contrasena, dto.Nombre, dto.Apellido, dto.Telefono, dto.id_rol), Times.Once);
    }



    /// <summary>
    /// Verifica que GetUsuarioByIdHandler lance KeyNotFoundException cuando el usuario no existe
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_Should_ThrowKeyNotFoundException_When_UsuarioDoesNotExist()
    {
        // Arrange
        var repo = new Mock<IUsuarioRepository>();

        // Configura el mock para lanzar una excepción si no encuentra el usuario
        repo.Setup(r => r.GetByIdAsync(999)).ThrowsAsync(new KeyNotFoundException("Usuario no encontrado."));

        var roleServiceMock = new Mock<IRoleService>();
        roleServiceMock.Setup(r => r.GetRoleNameByIdAsync(It.IsAny<int>())).ReturnsAsync("Usuario");

        var mapper = new Mock<IMapper>();
        var handler = new GetUsuarioByIdHandler(repo.Object, mapper.Object, roleServiceMock.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
          await handler.Handle(new GetUsuarioByIdQuery(999), default));

        // Assert
        Assert.Equal("Usuario no encontrado.", exception.Message);
    }



    /// <summary>
    /// Verifica que UpdateUsuarioHandler actualice el usuario correctamente
    /// </summary>
    [Fact]
    public async Task UpdateAsync_Should_ReturnUpdatedUsuario_When_UsuarioExists()
    {
        // Arrange
        var updateDto = new UsuarioUpdateDTO { Nombre = "Ana M", Email = "ana.new@example.com" };
        var usuarioExistente = new Usuario { id_usuario = 1, nombre = "Ana", email = "ana@example.com", id_rol = 2 };

        var repoMock = new Mock<IUsuarioRepository>();
        repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(usuarioExistente);
        repoMock.Setup(r => r.GetByEmailAsync(updateDto.Email)).ReturnsAsync((Usuario?)null);
        repoMock.Setup(r => r.UpdateAsync(1, It.IsAny<Usuario>())).ReturnsAsync(usuarioExistente);

        var roleServiceMock = new Mock<IRoleService>();
        roleServiceMock.Setup(r => r.GetRoleNameByIdAsync(It.IsAny<int>())).ReturnsAsync("Usuario");

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map(updateDto, usuarioExistente)).Callback<UsuarioUpdateDTO, Usuario>((src, dest) => { dest.nombre = src.Nombre; dest.email = src.Email; });
        mapperMock.Setup(m => m.Map<UsuarioDTO>(usuarioExistente)).Returns(new UsuarioDTO { id_usuario = "1", Nombre = updateDto.Nombre, Email = updateDto.Email, id_rol = 2 });

        var handler = new UpdateUsuarioHandler(repoMock.Object, mapperMock.Object, roleServiceMock.Object);

        // Act
        var result = await handler.Handle(new UpdateUsuarioCommand(1, updateDto), default);

        // Assert
        Assert.Equal("Ana M", result.Nombre);
        Assert.Equal("ana.new@example.com", result.Email);
        repoMock.Verify(r => r.UpdateAsync(1, It.IsAny<Usuario>()), Times.Once);
    }


    /// <summary>
    /// Verifica que DeleteUsuarioHandler elimine un usuario existente correctamente
    /// </summary>
    [Fact]
    public async Task DeleteAsync_Should_ReturnTrue_When_UsuarioExists()
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
        Assert.True(result);
    }

    /// <summary>
    /// Verifica que GetAllUsuariosHandler devuelva todos los usuarios correctamente
    /// </summary>
    [Fact]
    public async Task GetAllAsync_Should_ReturnAllUsuarios_When_Called()
    {
        // Arrange
        var usuarios = new[] { new Usuario { id_usuario = 1, nombre = "Ana" }, new Usuario { id_usuario = 2, nombre = "Juan" } };

        var repoMock = new Mock<IUsuarioRepository>();
        repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(usuarios);

        var roleServiceMock = new Mock<IRoleService>();
        roleServiceMock.Setup(r => r.GetRoleNameByIdAsync(It.IsAny<int>())).ReturnsAsync("Usuario");

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<IEnumerable<UsuarioDTO>>(usuarios))
        .Returns(usuarios.Select(u => new UsuarioDTO { id_usuario = u.id_usuario.ToString(), Nombre = u.nombre }));

        var handler = new GetAllUsuariosHandler(repoMock.Object, mapperMock.Object, roleServiceMock.Object);

        // Act
        var result = await handler.Handle(new GetAllUsuariosQuery(), default);

        // Assert
        Assert.Equal(2, result.Count());
    }


}
