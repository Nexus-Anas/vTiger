using APIntegro.Application.Common.Errors;
using APIntegro.Application.IHandlers;
using APIntegro.Application.Interfaces;
using AutoMapper;


namespace APIntegro.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAuthHandler _authHandler;
    private readonly IMapper _mapper;
    private LoginSession _session;

    public AuthenticationService(IAuthHandler authHandler, IMapper mapper, LoginSession session)
    {
        _authHandler = authHandler;
        _mapper = mapper;
        _session = session;
    }

    public async Task<AuthenticationResponse> Login(LoginRequest loginRequest)
    {

        var authResult = await _authHandler.Login(loginRequest);
        var user = _mapper.Map<User>(authResult.Result);
        _session.User = user;

        return authResult;

    }

    public async Task<AuthenticationResponse> Logout(string SessionName)
    {
        return await _authHandler.Logout(SessionName);
    }
}
