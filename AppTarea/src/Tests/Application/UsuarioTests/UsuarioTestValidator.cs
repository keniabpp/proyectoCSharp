using Application.Features.Usuarios.Commands;
using Application.Features.Usuarios.DTOs;
using Application.Features.Usuarios.Queries;
using Application.Features.Usuarios.Validator;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Tests.Application.UsuarioTests;

public class UsuarioValidatorTests
{
    /// <summary>
    /// Verifica que RegisterUsuarioCommandValidator valide correctamente cuando el email ya existe
    /// </summary>
    [Fact]
    public async Task Validate_Should_HaveErrors_When_EmailAlreadyExists()
    {
        // Arrange
        var repoMock = new Mock<IUsuarioRepository>();

        repoMock.Setup(r => r.GetByEmailAsync("ana@example.com")).ReturnsAsync(new Usuario());

        var validator = new RegisterUsuarioCommandValidator(repoMock.Object);

        var registerDto = new UsuarioRegisterDTO
        {
            Nombre = "Ana",
            Apellido = "Pérez",
            Telefono = "123456",
            Email = "ana@example.com",
        };

        var command = new RegisterUsuarioCommand(registerDto);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UsuarioRegisterDTO.Email).WithErrorMessage("El correo ya está registrado");

        result.ShouldHaveValidationErrorFor(x => x.UsuarioRegisterDTO.Contrasena);
    }


    /// <summary>
    /// Verifica que LoginUsuarioCommandValidator valide correctamente cuando el email está vacío
    /// </summary>
    [Fact]
    public async Task Validate_Should_HaveErrors_When_EmailIsEmpty()
    {
        // Arrange
        var validator = new LoginUsuarioCommandValidator();

        var loginDto = new UsuarioLoginDTO
        {
            Email = "",
            Contrasena = "pass"
        };

        var command = new LoginUsuarioCommand(loginDto);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UsuarioLoginDTO.Email).WithErrorMessage("El email es obligatorio");

        result.ShouldHaveValidationErrorFor(x => x.UsuarioLoginDTO.Contrasena);
    }



    /// <summary>
    /// Verifica que UpdateUsuarioCommandValidator valide cuando el usuario no existe o el email está duplicado
    /// </summary>
    [Fact]
    public async Task Validate_Should_HaveErrors_When_UsuarioNotFoundOrEmailDuplicated()
    {
        // Arrange
        var repoMock = new Mock<IUsuarioRepository>();


        repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Usuario?)null);


        repoMock.Setup(r => r.GetByEmailAsync("otro@example.com")).ReturnsAsync(new Usuario { id_usuario = 2 });

        var validator = new UpdateUsuarioCommandValidator(repoMock.Object);

        var updateDto = new UsuarioUpdateDTO
        {
            Nombre = "Ana",
            Apellido = "Pérez",
            Telefono = "123456",
            Email = "otro@example.com",
            Contrasena = ""
        };

        var command = new UpdateUsuarioCommand(1, updateDto);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id).WithErrorMessage("usuario no encontrado");

        result.ShouldHaveValidationErrorFor(x => x.UsuarioUpdateDTO.Email).WithErrorMessage("El correo ya está registrado por otro usuario.");
    }

    /// <summary>
    /// Verifica que CreateUsuarioCommandValidator valide cuando el email ya existe y la contraseña está vacía
    /// </summary>
    [Fact]
    public async Task Validate_Should_HaveErrors_When_EmailExistsAndPasswordIsEmpty()
    {
        // Arrange
        var repoMock = new Mock<IUsuarioRepository>();

        repoMock.Setup(r => r.GetByEmailAsync("ana@example.com")).ReturnsAsync(new Usuario());

        var validator = new RegisterUsuarioCommandValidator(repoMock.Object);

        var registerDto = new UsuarioRegisterDTO
        {
            Nombre = "Ana",
            Apellido = "Pérez",
            Telefono = "123456",
            Email = "ana@example.com",
        };

        var command = new RegisterUsuarioCommand(registerDto);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UsuarioRegisterDTO.Email).WithErrorMessage("El correo ya está registrado");

        result.ShouldHaveValidationErrorFor(x => x.UsuarioRegisterDTO.Contrasena);
    }
    
   
}
