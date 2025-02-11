using Newtonsoft.Json.Linq;

namespace APIntegro.Application.IHandlers;

public interface IApiHandler
{
    Task<JObject?> GetApiResponse(Dictionary<string, string> queryParams);
    Task<JObject?> PostApiResponse(Dictionary<string, string> queryParams);
}