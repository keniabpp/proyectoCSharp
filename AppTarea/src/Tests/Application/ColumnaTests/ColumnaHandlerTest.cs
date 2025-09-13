
using Application.Features.Columnas.DTOs;
using Application.Features.Columnas.Handlers;
using Application.Features.Columnas.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

public class ColumnaHandlerTests
{

    [Fact]
    public async Task GetByIdColumna()
    {
        // Arrange
        var repo = new Mock<IColumnaRepository>();

        // Configura el mock para lanzar una excepción si no encuentra el usuario
        repo.Setup(r => r.GetByIdAsync(999)).ThrowsAsync(new KeyNotFoundException("Columna no encontrada."));

        var mapper = new Mock<IMapper>();
        var handler = new GetColumnaByIdHandler(repo.Object, mapper.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
        await handler.Handle(new GetColumnaByIdQuery(999), default));

        // Assert
        Assert.Equal("Columna no encontrada.", exception.Message);
    }

    [Fact]
    public async Task GetAllColumnas()
    {
        var columnas = new[] { new Columna { id_columna = 1, nombre = "porhacer" }, new Columna { id_columna = 2, nombre = "porhacer" } };

        var repoMock = new Mock<IColumnaRepository>();
        repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(columnas);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<IEnumerable<ColumnaDTO>>(columnas))
        .Returns(columnas.Select(t => new ColumnaDTO { id_columna = t.id_columna, nombre = t.nombre }));

        var handler = new GetAllColumnasHandler(repoMock.Object, mapperMock.Object);

        var result = await handler.Handle(new GetAllColumnasQuery(), default);

        Assert.Equal(2, result.Count());
    }

}