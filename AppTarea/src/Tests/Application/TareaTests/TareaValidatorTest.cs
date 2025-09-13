using Xunit;
using Moq;
using FluentValidation.TestHelper;
using Application.Features.Tareas.Commands;
using Application.Features.Tareas.DTOs;
using Application.Features.Tareas.Validator;
using Domain.Interfaces;
using Domain.Entities;
using System.Threading.Tasks;
using Application.Features.Tareas.Queries;

public class TareaValidatorTests
{
    [Fact]
    public async Task CreatedTarea()
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

    [Fact]
    public async Task DeleteTarea()
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

    [Fact]
    public async Task TareaById()
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

    [Fact]
    public async Task MoverTareaColumna()
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

    [Fact]
    public async Task UpdateTarea()
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
