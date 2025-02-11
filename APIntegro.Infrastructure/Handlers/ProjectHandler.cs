using APIntegro.Application.Common.Errors;
using APIntegro.Application.IHandlers;
using APIntegro.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APIntegro.Infrastructure.Handlers;

public class ProjectHandler : IProjectHandler
{
    private readonly IApiHandler _apiHandler;
    public ProjectHandler(IApiHandler apiHandler) => _apiHandler = apiHandler;




    public async Task<Project> GetProject(string sessionName, string projectId)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "retrieve" },
            { "sessionName", sessionName },
            { "id", projectId }
        };

        var response = await _apiHandler.GetApiResponse(urlParams);

        ProcessResponseForErrors(response);

        return response["result"].ToObject<Project>();
    }

    public async Task<IEnumerable<Project>> GetAllProjects(string sessionName)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "query" },
            { "sessionName", sessionName },
            { "query", "SELECT * FROM Project;" }
        };

        var response = await _apiHandler.GetApiResponse(urlParams);

        ProcessResponseForErrors(response);

        return response["result"].ToObject<IEnumerable<Project>>();
    }

    public async Task<Project> PostProject(string sessionName, Project project)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "create" },
            { "sessionName", sessionName },
            { "element", JsonConvert.SerializeObject(project) },
            { "elementType", "Project" }
        };

        var response = await _apiHandler.PostApiResponse(urlParams);

        ProcessResponseForErrors(response);

        return response["result"].ToObject<Project>();
    }

    public async Task<Project> PutProject(string sessionName, Project project)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "update" },
            { "sessionName", sessionName },
            { "element", JsonConvert.SerializeObject(project) },
            { "elementType", "Project" }
        };

        var response = await _apiHandler.PostApiResponse(urlParams);

        ProcessResponseForErrors(response);

        return response["result"].ToObject<Project>();
    }

    public async Task<bool> RemoveProject(string sessionName, string projectId)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "delete" },
            { "sessionName", sessionName },
            { "id", projectId }
        };

        var response = await _apiHandler.PostApiResponse(urlParams);

        ProcessResponseForErrors(response);

        return response["success"].Value<bool>();
    }



    private static void ProcessResponseForErrors(JObject response)
    {
        if (response is null || !(response["success"]?.Value<bool>() ?? false))
        {
            var errorObject = response?["error"];
            var errorMessage = errorObject?["message"]?.Value<string>();
            var errorCode = errorObject?["code"]?.Value<string>();

            if (!string.IsNullOrEmpty(errorCode) && !string.IsNullOrEmpty(errorMessage))
                throw new VtigerException(errorCode, errorMessage);
            else
                throw new VtigerException("UnknownError", "An unknown error occurred.");
        }
    }
}