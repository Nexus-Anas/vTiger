using APIntegro.Application.Common.Errors;
using APIntegro.Application.IHandlers;
using APIntegro.Application.Services.ProjectTasks;
using APIntegro.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APIntegro.Infrastructure.Handlers;

public class ProjectTaskHandler : IProjectTaskHandler
{
    private readonly IApiHandler _apiHandler;
    public ProjectTaskHandler(IApiHandler apiHandler) => _apiHandler = apiHandler;




    public async Task<ProjectTask> GetProjectTask(string sessionName, string projectTaskId)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "retrieve" },
            { "sessionName", sessionName },
            { "id", projectTaskId }
        };

        var response = await _apiHandler.GetApiResponse(urlParams);

        ProcessResponseForErrors(response);

        return response["result"].ToObject<ProjectTask>();
    }

    public async Task<IEnumerable<ProjectTask>> GetAllProjectTasks(string sessionName)
    {
        var urlParams = new Dictionary<string, string>
    {
        { "operation", "query" },
        { "sessionName", sessionName },
        { "query", "SELECT * FROM ProjectTask;" }
    };

        var response = await _apiHandler.GetApiResponse(urlParams);

        ProcessResponseForErrors(response);

        return response["result"].ToObject<IEnumerable<ProjectTask>>();
    }

    public async Task<ProjectTask> PostProjectTask(string SessionName, ProjectTask ProjectTask)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "create" },
            { "sessionName", SessionName },
            { "element", JsonConvert.SerializeObject(ProjectTask) },
            { "elementType", "ProjectTask" }
        };

        var response = await _apiHandler.PostApiResponse(urlParams);

        ProcessResponseForErrors(response);

        return response["result"].ToObject<ProjectTask>();
    }

    public async Task<ProjectTask> PutProjectTask(string SessionName, ProjectTask ProjectTask)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "update" },
            { "sessionName", SessionName },
            { "element", JsonConvert.SerializeObject(ProjectTask) },
            { "elementType", "ProjectTask" }
        };

        var response = await _apiHandler.PostApiResponse(urlParams);

        ProcessResponseForErrors(response);

        return response["result"].ToObject<ProjectTask>();
    }

    public async Task<bool> RemoveProjectTask(string sessionName, string projectTaskId)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "delete" },
            { "sessionName", sessionName },
            { "id", projectTaskId }
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