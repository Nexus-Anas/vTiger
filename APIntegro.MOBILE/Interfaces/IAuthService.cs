using APIntegro.MOBILE.Data;
using APIntegro.MOBILE.ViewModels.Authentication;

namespace APIntegro.MOBILE.Interfaces;

public interface IAuthService
{
    Task<User> Login(LoginVM request);
}