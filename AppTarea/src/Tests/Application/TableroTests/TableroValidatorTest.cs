
using FluentValidation.TestHelper;
using Application.Features.Tableros.Commands;
using Application.Features.Tableros.DTOs;
using Application.Features.Tableros.Validator;
using Domain.Interfaces;
using Moq;
using Domain.Entities;
public class TableroTestsValidator
{
    [Fact]
    public async Task CreateTablero()
    {
        // Arrange
        var validator = new CreateTableroCommandValidator();
        var command = new CreateTableroCommand(
            new TableroCreateDTO
            {
                nombre = "" // Para probar validación
            }
        );

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.TableroCreateDTO.nombre).WithErrorMessage("El nombre del tablero es obligatorio");
    }

    [Fact]
    public async Task DeleteTablero()
    {
        // Arrange
        var repo = new Mock<ITableroRepository>();
        repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Tablero?)null); // no existe
        var validator = new DeleteTableroCommandValidator(repo.Object);

        var command = new DeleteTableroCommand(1);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id).WithErrorMessage("El Tablero no existe");
    }


    




}