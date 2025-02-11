using APIntegro.API.Controllers;
using APIntegro.Application.Common.Errors;
using APIntegro.Application.Interfaces;
using APIntegro.Application.Services.Authentication;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace APIntegro.API.UnitTests.Controllers;

public class AuthenticationControllerTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IAuthenticationService> _authenticationServiceMock;
    private readonly AuthenticationController _sut;

    public AuthenticationControllerTests()
    {
        _fixture = new Fixture();
        _authenticationServiceMock = _fixture.Freeze<Mock<IAuthenticationService>>();
        _sut = new AuthenticationController(_authenticationServiceMock.Object);
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsOkResponse()
    {
        //Arrange
        var loginRequestMock = _fixture.Create<LoginRequest>();
        var loginResponseMock = _fixture.Create<AuthenticationResponse>();

        _authenticationServiceMock.Setup(x => x.Login(loginRequestMock)).ReturnsAsync(loginResponseMock);

        //Act
        var result = await _sut.Login(loginRequestMock);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<ActionResult<AuthenticationResponse>>();
        result.Result.Should().BeAssignableTo<OkObjectResult>();
        result.Result.As<OkObjectResult>().Value
            .Should()
            .NotBeNull()
            .And.BeOfType(loginResponseMock.GetType());

        _authenticationServiceMock.Verify(x => x.Login(loginRequestMock), Times.Once);
    }

    [Fact]
    public async Task Login_InvalidCredentials_ReturnsBadRequest()
    {
        // Arrange
        var loginRequestMock = _fixture.Create<LoginRequest>();

        _authenticationServiceMock.Setup(x => x.Login(It.IsAny<LoginRequest>()))
            .ThrowsAsync(new VtigerException("errorCode", "errorMessage"));

        // Act
        var result = await _sut.Login(loginRequestMock);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<BadRequestObjectResult>(); 
        _authenticationServiceMock.Verify(x => x.Login(It.IsAny<LoginRequest>()), Times.Once);
    }


}
