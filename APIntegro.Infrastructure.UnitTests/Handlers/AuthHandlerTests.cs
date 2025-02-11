using APIntegro.Application.Common.Errors;
using APIntegro.Application.IHandlers;
using APIntegro.Application.Services.Authentication;
using APIntegro.Infrastructure.Handlers;
using Moq;
using Newtonsoft.Json.Linq;

namespace APIntegro.Infrastructure.UnitTests.Handlers;

public class AuthHandlerTests
{
    private readonly Mock<IApiHandler> mockApiHandler;
    private readonly IApiHandler _apiHandler;

    public AuthHandlerTests()
    {
        mockApiHandler = new Mock<IApiHandler>();
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsAuthenticationResponse()
    {
        // Arrange 
        var mockApiHandler = new Mock<IApiHandler>();
        var mockChallengeResponse = new ChallengeResponse(
                true,
                new ChallengeResult(
                        "challenge_token",
                        "serverTime",
                        "expireTime"
                    )
            );

        var expectedUsername = "test_user";
        var expectedAccessKey = "your_access_key";

        //mockApiHandler.Setup(x => x.GetApiResponse(It.IsAny<Dictionary<string, string>>()))
        // .ReturnsAsync(mockChallengeResponse);

        var authHandler = new AuthHandler(mockApiHandler.Object); 

        var loginRequest = new LoginRequest(expectedUsername, expectedAccessKey);
        var loginResponse = await authHandler.Login(loginRequest);

        Assert.NotNull(loginResponse);

        // Verify calls to mocked dependencies
        mockApiHandler.Verify(x => x.PostApiResponse(It.Is<Dictionary<string, string>>(d =>
            d["operation"] == "login" &&
            d["username"] == expectedUsername &&
            d["accessKey"] == authHandler.GetMd5Hash($"{mockChallengeResponse.Result.Token}{expectedAccessKey}"))));
    }

    //[Fact]
    //public async Task Login_InvalidChallenge_ThrowsVtigerException()
    //{
    //    // Arrange
    //    var mockApiHandler = new Mock<IApiHandler>();
    //    var mockChallengeToken = new Dictionary<string, bool>() { { "success", false } };
    //    var expectedErrorMessage = "Challenge failed";
    //    var expectedErrorCode = "CHALLENGE_ERROR";

    //    mockApiHandler.Setup(x => x.PostApiResponse(It.IsAny<Dictionary<string, string>>())).Returns(Task.FromResult(new object())); // Don't call actual API

    //    var loginService = new YourLoginService(mockApiHandler.Object);

    //    // Act & Assert
    //    var loginRequest = new LoginRequest();
    //    await Assert.ThrowsAsync<VtigerException>(async () => await loginService.Login(loginRequest));

    //    // Verify calls to mocked dependencies
    //    mockApiHandler.Verify(x => x.PostApiResponse(It.IsAny<Dictionary<string, string>>()), Times.Never); // Shouldn't call actual API
    //}

    //[Fact]
    //public async Task Login_InvalidLogin_ThrowsVtigerException()
    //{
    //    // Arrange (Similar to Login_ValidCredentials with a failing login response)
    //    var mockApiHandler = new Mock<IApiHandler>();
    //    var mockChallengeToken = new Dictionary<string, bool>() { { "success", true } };
    //    var mockChallengeResponse = new ChallengeResponse() { Result = new { Token = "challenge_token" } };
    //    var expectedUsername = "test_user";
    //    var expectedAccessKey = "your_access_key";

    //    mockApiHandler.Setup(x => x.PostApiResponse(It.IsAny<Dictionary<string, string>>()))
    //        .ReturnsAsync(Task.FromResult(new Dictionary<string, object>() { { "success", false } }));

    //    var loginService = new YourLoginService(mockApiHandler.Object);

    //    var loginRequest = new LoginRequest() { UserName = expectedUsername, AccessKey = expectedAccessKey };

    //    // Act & Assert
    //    var expectedErrorMessage = "Login failed";
    //    var expectedErrorCode = "LOGIN_ERROR";

    //    await Assert.ThrowsAsync<VtigerException>(async () => await loginService.Login(loginRequest));

    //    // Verify calls to mocked dependencies (similar to Login_ValidCredentials)
    //}

    [Fact]
    public void GetMd5Hash_EmptyInput_ReturnsEmptyString()
    {
        // Arrange
        var mockApiHandler = new Mock<IApiHandler>();

        var authHandler = new AuthHandler(mockApiHandler.Object);

        // Act
        var result = authHandler.GetMd5Hash(null);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void GetMd5Hash_ValidInput_ReturnsCorrectHash()
    {
        // Arrange
        var mockApiHandler = new Mock<IApiHandler>();

        var authHandler = new AuthHandler(mockApiHandler.Object);
        
        string inputString = "This_is_a_test_string";
        string expectedHash = "09b9e9666b3156b4d31789679fb27136"; 

        // Act
        var result = authHandler.GetMd5Hash(inputString);

        // Assert
        Assert.Equal(expectedHash, result);
    }





}
