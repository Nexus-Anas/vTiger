using APIntegro.WEB.Data;
using APIntegro.WEB.Interfaces;
using APIntegro.WEB.ViewModels.Authentication;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;

namespace APIntegro.WEB.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly string? _baseUrl;
    private string _controller = "Authentication";

    public AuthService(HttpClient http, IConfiguration configuration)
    {
        _http = http;
        _baseUrl = configuration.GetValue<string>("ApiSettings:APIntegroUrl");
    }



    
    public async Task<User> Login(LoginVM request)
    {
        var response = await _http.PostAsJsonAsync($"{_baseUrl}{_controller}/login", request);

        if (response.StatusCode != HttpStatusCode.OK) return null;

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<JObject>(content);

        return result["result"].ToObject<User>();
    }
}