using APIntegro.Application.IHandlers;
using APIntegro.Infrastructure.Handlers;
using Moq;

namespace APIntegro.Infrastructure.UnitTests.Handlers;

public class ProjectHandlerTest
{
    private readonly Mock<IApiHandler> _apiHandlerMock;
    private readonly IProjectHandler _projectHandler;

    public ProjectHandlerTest()
    {
        _apiHandlerMock = new Mock<IApiHandler>();
        _projectHandler = new ProjectHandler(_apiHandlerMock.Object);
    }

    [Fact]
    public async Task GetProject_ValidInput_ReturnsProject()
    {
        // Arrange
        var sessionName = "41252fb466267f7b2d4bb";
        var projectId = "29x6";

        // Act
        var result = await _projectHandler.GetProject(sessionName, projectId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(projectId, result.id);
    }
}