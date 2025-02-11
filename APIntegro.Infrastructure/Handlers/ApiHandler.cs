using APIntegro.Application.IHandlers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using APIntegro.Application.Common.Errors;

namespace APIntegro.Infrastructure.Handlers;

public class ApiHandler : IApiHandler
{
    private readonly string? _baseUrl;

    public ApiHandler()
    {
    }

    public ApiHandler(IConfiguration configuration)
        => _baseUrl = configuration.GetSection("ApiSettings:BaseUrl").Value;




    public async Task<JObject?> GetApiResponse(Dictionary<string, string> queryParams)
    {
        using var client = new HttpClient();
        string url = $"{_baseUrl}?{string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"))}";

        var response = await client.GetAsync(url);
        return await HandleApiResponse(response);
    }

    public async Task<JObject?> PostApiResponse(Dictionary<string, string> queryParams)
    {
        using var client = new HttpClient();
        HttpContent content = queryParams is not null
            ? new FormUrlEncodedContent(queryParams)
            : new StringContent("");

        var response = await client.PostAsync(_baseUrl, content);
        return await HandleApiResponse(response);
    }




    private async Task<JObject?> HandleApiResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JObject>(responseContent);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            var errorObject = JsonConvert.DeserializeObject<JObject>(errorContent);

            var error = errorObject?["error"];
            var errorMessage = error?["message"]?.Value<string>();
            var errorCode = error?["code"]?.Value<string>();

            if (!string.IsNullOrEmpty(errorCode) && !string.IsNullOrEmpty(errorMessage))
                throw new VtigerException(errorCode, errorMessage);
            else
                throw new VtigerException("UnknownError", "An unknown error occurred.");
        }
    }
}