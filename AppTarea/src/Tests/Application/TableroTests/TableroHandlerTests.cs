
using Moq;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Application.Features.Tableros.Commands;
using Application.Features.Tableros.Handlers;
using Application.Features.Tableros.DTOs;
using Application.Features.Tableros.Queries;

public class TableroHandlerTests
{
    [Fact]
    public async Task CreateTablero()
    {
        // Arrange
        var dto = new TableroCreateDTO { nombre = "Tablero Prueba", creado_por = 1, id_rol = 1 };
        var tableroCreado = new Tablero { id_tablero = 1, nombre = dto.nombre, creado_por = dto.creado_por, id_rol = dto.id_rol };

        var repoMock = new Mock<ITableroRepository>();
        repoMock.Setup(r => r.CreateAsync(It.IsAny<Tablero>())).ReturnsAsync(tableroCreado);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<Tablero>(dto)).Returns(tableroCreado);
        mapperMock.Setup(m => m.Map<TableroDTO>(tableroCreado))
        .Returns(new TableroDTO { id_tablero = tableroCreado.id_tablero, nombre = tableroCreado.nombre });

        var handler = new CreateTableroHandler(repoMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(new CreateTableroCommand(dto), default);

        // Assert
        Assert.Equal(tableroCreado.id_tablero, result.id_tablero);
        Assert.Equal(tableroCreado.nombre, result.nombre);
    }

    [Fact]
    public async Task DeleteTablero()
    {
        // Arrange
        var tableroExistente = new Tablero
        {
            id_tablero = 1,
            nombre = "prueba",

        };

        var repo = new Mock<ITableroRepository>();
        // Simulamos que el usuario existe
        repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(tableroExistente);
        // Simulamos que la eliminación es exitosa
        repo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

        var handler = new DeleteTableroHandler(repo.Object);

        // Act
        var result = await handler.Handle(new DeleteTableroCommand(1), default);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateTablero()
    {
        // Arrange
        var updateDto = new TableroUpdateDTO { nombre = "fin" };
        var tableroExistente = new Tablero { id_tablero = 1 };

        var repoMock = new Mock<ITableroRepository>();
        repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(tableroExistente);
        repoMock.Setup(r => r.UpdateAsync(1, It.IsAny<Tablero>())).ReturnsAsync(tableroExistente);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map(updateDto, tableroExistente)).Callback<TableroUpdateDTO, Tablero>((src, dest) => { dest.nombre = src.nombre; });
        mapperMock.Setup(m => m.Map<TableroDTO>(tableroExistente)).Returns(new TableroDTO { id_tablero = 1, nombre = updateDto.nombre });

        var handler = new UpdateTableroHandler(repoMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(new UpdateTableroCommand(1, updateDto), default);

        // Assert
        Assert.Equal("fin", result.nombre);

        repoMock.Verify(r => r.UpdateAsync(1, It.IsAny<Tablero>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdTablero()
    {
        // Arrange
        var repo = new Mock<ITableroRepository>();

        // Configura el mock para lanzar una excepción si no encuentra el usuario
        repo.Setup(r => r.GetByIdAsync(999)).ThrowsAsync(new KeyNotFoundException("Tablero no encontrado."));

        var mapper = new Mock<IMapper>();
        var handler = new GetTableroByIdHandler(repo.Object, mapper.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
          await handler.Handle(new GetTableroByIdQuery(999), default));

        // Assert
        Assert.Equal("Tablero no encontrado.", exception.Message);
    }

    
    [Fact]
    public async Task GetAllTableros()
    {
        var tableros = new[] { new Tablero { id_tablero = 1, nombre = "fin" }, new Tablero { id_tablero = 2, nombre = "prueba" } };

        var repoMock = new Mock<ITableroRepository>();
        repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(tableros);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<IEnumerable<TableroDTO>>(tableros))
        .Returns(tableros.Select(t => new TableroDTO { id_tablero = t.id_tablero, nombre = t.nombre }));

        var handler = new GetAllTablerosHandler(repoMock.Object, mapperMock.Object);

        var result = await handler.Handle(new GetAllTablerosQuery(), default);

        Assert.Equal(2, result.Count());
    }

}
