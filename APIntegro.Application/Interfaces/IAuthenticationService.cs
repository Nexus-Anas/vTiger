using APIntegro.Application.Services.Authentication;

namespace APIntegro.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Login(LoginRequest loginRequest);
        Task<AuthenticationResponse> Logout(string SessionName);
    }
}