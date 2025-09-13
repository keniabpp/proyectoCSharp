
using Moq;
using Application.Features.Tareas.Commands;
using Application.Features.Tareas.DTOs;
using Application.Features.Tareas.Handlers;
using Domain.Entities;
using Domain.Interfaces;
using AutoMapper;
using Application.Features.Tareas.Queries;
using Domain.Enums;

public class TareaHandlerTests
{
    [Fact]
    public async Task CreatedTarea()
    {
        // Arrange
        var dto = new TareaCreateDTO
        {
            titulo = "Tarea Prueba",
            descripcion = "Descripción",
            creado_por = 1,
            asignado_a = 2,
            id_tablero = 3,
            id_columna = 1
        };

        var tarea = new Tarea { id_tarea = 1, titulo = dto.titulo };

        var usuario = new Usuario { id_usuario = 1, nombre = "Pablo" };
        var tablero = new Tablero { id_tablero = 3, nombre = "Tablero" };
        var columna = new Columna { id_columna = 1, nombre = "PorHacer" };

        var tareaRepo = new Mock<ITareaRepository>();
        tareaRepo.Setup(r => r.CreateAsync(It.IsAny<Tarea>())).ReturnsAsync(tarea);

        var usuarioRepo = new Mock<IUsuarioRepository>();
        usuarioRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(usuario);

        var tableroRepo = new Mock<ITableroRepository>();
        tableroRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(tablero);

        var columnaRepo = new Mock<IColumnaRepository>();
        columnaRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(columna);

        var mapper = new Mock<IMapper>();
        mapper.Setup(m => m.Map<Tarea>(dto)).Returns(tarea);
        mapper.Setup(m => m.Map<TareaDTO>(tarea)).Returns(new TareaDTO { id_tarea = 1, titulo = tarea.titulo });

        var handler = new CreateTareaHandler(
            tareaRepo.Object, mapper.Object, usuarioRepo.Object, columnaRepo.Object, tableroRepo.Object
        );

        // Act
        var result = await handler.Handle(new CreateTareaCommand(dto), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.id_tarea);
        Assert.Equal("Tarea Prueba", result.titulo);
    }


    [Fact]
    public async Task CreadorEliminaTarea()
    {
        // Arrange
        var tarea = new Tarea { id_tarea = 1, creado_por = 10 };
        var repoMock = new Mock<ITareaRepository>();
        repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(tarea);
        repoMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

        var handler = new DeleteTareaHandler(repoMock.Object);
        var command = new DeleteTareaCommand(1, 10);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.True(result);
        repoMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }



    [Fact]
    public async Task NoCreadorEliminaTarea()
    {
        // Arrange
        var tarea = new Tarea { id_tarea = 1, creado_por = 10 };
        var repoMock = new Mock<ITareaRepository>();
        repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(tarea);

        var handler = new DeleteTareaHandler(repoMock.Object);
        var command = new DeleteTareaCommand(1, 20);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, default));
        repoMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task GetAllTareas()
    {
        var tareas = new[] { new Tarea { id_tarea = 1, titulo = "fin" }, new Tarea { id_tablero = 2, descripcion = "prueba" } };

        var repoMock = new Mock<ITareaRepository>();
        repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(tareas);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<IEnumerable<TareaDTO>>(tareas))
        .Returns(tareas.Select(t => new TareaDTO { id_tarea = t.id_tarea, titulo = t.titulo, descripcion = t.descripcion }));

        var handler = new GetAllTareasHandler(repoMock.Object, mapperMock.Object);

        var result = await handler.Handle(new GetAllTareasQuery(), default);

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdTarea()
    {
        // Arrange
        var repo = new Mock<ITareaRepository>();

        // Configura el mock para lanzar una excepción si no encuentra el usuario
        repo.Setup(r => r.GetByIdAsync(999)).ThrowsAsync(new KeyNotFoundException("Tarea no encontrada."));

        var mapper = new Mock<IMapper>();
        var handler = new GetTareaByIdHandler(repo.Object, mapper.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
          await handler.Handle(new GetTareaByIdQuery(999), default));

        // Assert
        Assert.Equal("Tarea no encontrada.", exception.Message);
    }

    [Fact]
    public async Task UpdateTareaCreador()
    {
        // Arrange
        var tareaRepoMock = new Mock<ITareaRepository>();
        var mapperMock = new Mock<IMapper>();

        var tareaExistente = new Tarea { id_tarea = 1, creado_por = 10, titulo = "prueba" };
        var updateDto = new TareaUpdateDTO { titulo = "pruebas", descripcion = "hacer las pruebas" };
        var command = new UpdateTareaCommand(1, updateDto, 10);

        tareaRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(tareaExistente);

        mapperMock.Setup(m => m.Map(updateDto, tareaExistente));
        mapperMock.Setup(m => m.Map<TareaDTO>(tareaExistente)).Returns(new TareaDTO { id_tarea = 1, titulo = "Nuevo título", descripcion = "Nueva desc", detalle = "X", nombre_columna = "Y", estado_fechaVencimiento = "Z" });

        var handler = new UpdateTareaHandler(tareaRepoMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        tareaRepoMock.Verify(r => r.UpdateAsync(1, tareaExistente), Times.Once);
        Assert.Equal("Nuevo título", result.titulo);
        Assert.Equal("Nueva desc", result.descripcion);
    }

    [Fact]
    public async Task UpdateTareaNoCreador()
    {
        // Arrange
        var tareaRepoMock = new Mock<ITareaRepository>();
        var mapperMock = new Mock<IMapper>();

        var tareaExistente = new Tarea { id_tarea = 1, creado_por = 10, titulo = "prueba" };
        var updateDto = new TareaUpdateDTO { titulo = "pruebas", descripcion = "hacer las pruebas" };
        var command = new UpdateTareaCommand(1, updateDto, 20);

        tareaRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(tareaExistente);

        var handler = new UpdateTareaHandler(tareaRepoMock.Object, mapperMock.Object);

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
        () => handler.Handle(command, CancellationToken.None));

        // Assert
        Assert.Equal(" Solo el creador puede actualizar la tarea", exception.Message);
        tareaRepoMock.Verify(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<Tarea>()), Times.Never);
    }


    [Fact]
    public async Task TareasAsignadas()
    {
        var repo = new Mock<ITareaRepository>();
        var mapper = new Mock<IMapper>();

        repo.Setup(r => r.TareasAsignadasAsync(1))
        .ReturnsAsync(new List<Tarea> { new Tarea { id_tarea = 1, titulo = "Tarea prueba" } });

        mapper.Setup(m => m.Map<List<TareaDTO>>(It.IsAny<List<Tarea>>()))
        .Returns(new List<TareaDTO> { new TareaDTO { id_tarea = 1, titulo = "Tarea prueba", descripcion = "d", detalle = "x", nombre_columna = "c", estado_fechaVencimiento = "e" } });

        var handler = new GetTareasAsignadasHandler(repo.Object, mapper.Object);

        var result = await handler.Handle(new GetTareasAsignadasQuery(1), default);

        Assert.Single(result);
        Assert.Equal("Tarea prueba", result[0].titulo);
    }

    [Fact]
    public async Task TareaMovidaCorrectamente()
    {
        // Arrange
        var tareaRepo = new Mock<ITareaRepository>();
        var columnaRepo = new Mock<IColumnaRepository>();

        var tarea = new Tarea
        {
            id_tarea = 1,
            asignado_a = 5,
            id_columna = 1,
            fecha_vencimiento = DateTime.Now.AddDays(1)
        };
        var columna = new Columna { id_columna = 1, posicion = EstadoColumna.PorHacer };

        tareaRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(tarea);
        columnaRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(columna);
        tareaRepo.Setup(r => r.MoverTareaAsync(1, 2)).ReturnsAsync(true);

        var handler = new MoverTareaHandler(tareaRepo.Object, columnaRepo.Object);
        var command = new MoverTareaCommand(new MoverTareaDTO { id_tarea = 1, id_columna = 2, detalle = "Nuevo detalle" }, 5);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.True(result);
        Assert.Equal("Nuevo detalle", tarea.detalle);
        tareaRepo.Verify(r => r.MoverTareaAsync(1, 2), Times.Once);
    }


}
