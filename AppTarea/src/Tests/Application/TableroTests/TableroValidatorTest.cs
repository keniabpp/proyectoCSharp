using Application.Features.Tableros.Commands;
using Application.Features.Tableros.DTOs;
using Application.Features.Tableros.Validator;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Tests.Application.TableroTests;

public class TableroValidatorTests
{
    /// <summary>
    /// Verifica que CreateTableroCommandValidator valide correctamente cuando el nombre está vacío
    /// </summary>
    [Fact]
    public async Task Validate_Should_HaveError_When_NombreIsEmpty()
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

    /// <summary>
    /// Verifica que DeleteTableroCommandValidator valide cuando el tablero no existe
    /// </summary>
    [Fact]
    public async Task Validate_Should_HaveError_When_TableroDoesNotExist()
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