using APIntegro.WEB.Data;
using APIntegro.WEB.ViewModels.Authentication;

namespace APIntegro.WEB.Interfaces;

public interface IAuthService
{
    Task<User> Login(LoginVM request);
}