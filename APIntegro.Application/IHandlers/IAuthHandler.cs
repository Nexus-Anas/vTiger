using APIntegro.Application.Services.Authentication;

namespace APIntegro.Application.IHandlers;

public interface IAuthHandler
{
    Task<AuthenticationResponse> Login(LoginRequest loginRequest);
    Task<AuthenticationResponse> Logout(string SessionName);
}
