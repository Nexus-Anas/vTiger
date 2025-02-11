using APIntegro.MOBILE.Data;
using APIntegro.MOBILE.Interfaces;
using APIntegro.MOBILE.ViewModels.Authentication;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace APIntegro.MOBILE.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly string? _baseUrl;
    private string _controller = "Authentication";

    public AuthService(HttpClient http, IConfiguration configuration)
    {
        _http = http;
        _baseUrl = "https://funmintski42.conveyor.cloud/api/";
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