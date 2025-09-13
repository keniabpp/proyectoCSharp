
using Xunit;
using Moq;
using FluentValidation.TestHelper;
using Application.Features.Usuarios.Commands;
using Application.Features.Usuarios.Validator;
using Domain.Entities;
using Domain.Interfaces;
using Application.Features.Usuarios.DTOs;
using Application.Features.Usuarios.Queries;



public class UsuarioTestsValidator
{
    [Fact]
    public async Task RegisterValidator()
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


    [Fact]
    public async Task LoginValidator()
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



    [Fact]
    public async Task UpdateValidator()
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

    [Fact]
    public async Task CreatedValidator()
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
