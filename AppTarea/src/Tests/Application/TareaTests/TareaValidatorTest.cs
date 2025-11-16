using Application.Features.Tareas.Commands;
using Application.Features.Tareas.DTOs;
using Application.Features.Tareas.Queries;
using Application.Features.Tareas.Validator;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Tests.Application.TareaTests;

public class TareaValidatorTests
{
    /// <summary>
    /// Verifica que CreateTareaCommandValidator no tenga errores cuando todos los datos son válidos
    /// </summary>
    [Fact]
    public async Task Validate_Should_NotHaveErrors_When_AllDataIsValid()
    {
        // Arrange
        var usuarioRepoMock = new Mock<IUsuarioRepository>();
        var tableroRepoMock = new Mock<ITableroRepository>();
        var columnaRepoMock = new Mock<IColumnaRepository>();

        // Simular que todo existe
        usuarioRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
        .ReturnsAsync(new Usuario { id_usuario = 1, nombre = "ttu" });
        tableroRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
        .ReturnsAsync(new Tablero { id_tablero = 1, nombre = "Tablero " });
        columnaRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
        .ReturnsAsync(new Columna { id_columna = 1, nombre = "PorHacer" });

        var validator = new CreateTareaCommandValidator(
            usuarioRepoMock.Object, tableroRepoMock.Object, columnaRepoMock.Object
        );

        var dto = new TareaCreateDTO
        {
            titulo = "Tarea ",
            descripcion = "Descripción ",
            creado_por = 1,
            asignado_a = 2,
            id_tablero = 1,
            id_columna = 1
        };
        var command = new CreateTareaCommand(dto);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Verifica que DeleteTareaCommandValidator no tenga errores cuando la tarea existe
    /// </summary>
    [Fact]
    public async Task Validate_Should_NotHaveErrors_When_TareaExists()
    {
        // Arrange
        var repoMock = new Mock<ITareaRepository>();
        repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Tarea { id_tarea = 1, titulo = "Tarea test" });

        var validator = new DeleteTareaCommandValidator(repoMock.Object);

        var command = new DeleteTareaCommand(1, 10);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Verifica que GetTareaByIdQueryValidator no tenga errores cuando la tarea existe
    /// </summary>
    [Fact]
    public async Task Validate_Should_NotHaveErrors_When_TareaExistsById()
    {
        // Arrange
        var repoMock = new Mock<ITareaRepository>();
        repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Tarea { id_tarea = 1, titulo = "Tarea test" });

        var validator = new GetTareaByIdQueryValidator(repoMock.Object);

        var query = new GetTareaByIdQuery(1);

        // Act
        var result = await validator.TestValidateAsync(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Verifica que MoverTareaCommandValidator no tenga errores cuando tarea y columna existen
    /// </summary>
    [Fact]
    public async Task Validate_Should_NotHaveErrors_When_TareaAndColumnaExist()
    {
        // Arrange
        var tareaRepoMock = new Mock<ITareaRepository>();
        tareaRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Tarea { id_tarea = 1 });

        var columnaRepoMock = new Mock<IColumnaRepository>();
        columnaRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Columna { id_columna = 1 });

        var validator = new MoverTareaCommandValidator(tareaRepoMock.Object, columnaRepoMock.Object);

        var command = new MoverTareaCommand(new MoverTareaDTO
        {
            id_tarea = 1,
            id_columna = 1,
            detalle = "Mover tarea"
        },  asignado_a: 10);

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Verifica que UpdateTareaCommandValidator no tenga errores cuando la tarea existe
    /// </summary>
    [Fact]
    public async Task Validate_Should_NotHaveErrors_When_TareaExistsForUpdate()
    {
        // Arrange
        var repoMock = new Mock<ITareaRepository>();
        repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
        .ReturnsAsync(new Tarea { id_tarea = 1 });

        var validator = new UpdateTareaCommandValidator(repoMock.Object);

        var command = new UpdateTareaCommand
        (
            1,
            new TareaUpdateDTO { titulo = "jjj", descripcion = "tjyj" },
            creado_por: 10
        );

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
