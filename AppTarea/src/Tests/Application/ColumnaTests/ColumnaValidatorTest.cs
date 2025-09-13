


using Application.Features.Columnas.Queries;
using Application.Features.Columnas.Validator;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation.TestHelper;
using Moq;

public class ColumnaValidatorTests
{

    [Fact]
    public async Task ColumnaById()
    {
        // Arrange
        var repoMock = new Mock<IColumnaRepository>();
        repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Columna { id_columna = 1, nombre = "porhacer" });

        var validator = new GetColumnaByIdQueryValidator(repoMock.Object);

        var query = new GetColumnaByIdQuery(1);

        // Act
        var result = await validator.TestValidateAsync(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

}