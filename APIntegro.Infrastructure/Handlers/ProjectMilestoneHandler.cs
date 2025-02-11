using APIntegro.Application.Common.Errors;
using APIntegro.Application.DTOs.ProjectMilestone;
using APIntegro.Application.IHandlers;
using APIntegro.Application.Services.ProjectMilestones;
using APIntegro.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APIntegro.Infrastructure.Handlers;

public class ProjectMilestoneHandler : IProjectMilestoneHandler
{
    private readonly IApiHandler _apiHandler;

    public ProjectMilestoneHandler(IApiHandler apiHandler)
    {
        _apiHandler = apiHandler;
    }

    public async Task<IEnumerable<ProjectMilestone>> GetAllProjectMilestones(string sessionName)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "query" },
            { "sessionName", sessionName },
            { "query", "SELECT * FROM ProjectMilestone;" }
        };

        var response = await _apiHandler.GetApiResponse(urlParams);

        ThrowIfError(response);

        var res =  response["result"].ToObject<IEnumerable<ProjectMilestone>>();
        return res;
    }

    public async Task<ProjectMilestone> GetProjectMilestone(string sessionName, string projectMilestoneId)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "retrieve" },
            { "sessionName", sessionName },
            { "id", projectMilestoneId }
        };

        var response = await _apiHandler.GetApiResponse(urlParams);

        ThrowIfError(response);

        return response["result"].ToObject<ProjectMilestone>();
    }

    public async Task<ProjectMilestone> PostProjectMilestone(string sessionName, CreateProjectMilestoneDto projectMilestone)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "create" },
            { "sessionName", sessionName },
            { "element", JsonConvert.SerializeObject(projectMilestone) },
            { "elementType", "ProjectMilestone" }
        };

        var response = await _apiHandler.PostApiResponse(urlParams);

        ThrowIfError(response);

        return response["result"].ToObject<ProjectMilestone>();
    }

    public async Task<ProjectMilestone> PutProjectMilestone(string sessionName, UpdateProjectMilestoneDto projectMilestone)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "update" },
            { "sessionName", sessionName },
            { "element", JsonConvert.SerializeObject(projectMilestone) },
            { "elementType", "ProjectMilestone" }
        };

        var response = await _apiHandler.PostApiResponse(urlParams);

        ThrowIfError(response);

        return response["result"].ToObject<ProjectMilestone>();
    }

    public async Task<bool> RemoveProjectMilestone(string sessionName, string projectMilestoneId)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "delete" },
            { "sessionName", sessionName },
            { "id", projectMilestoneId }
        };

        var response = await _apiHandler.PostApiResponse(urlParams);
        Console.WriteLine(response);

        ThrowIfError(response);

        return response["success"].Value<bool>();
    }

    static void ThrowIfError(JObject response)
    {
        if (response is null || !response["success"].Value<bool>())
        {
            var errorObject = response["error"];
            var errorMessage = errorObject["message"].Value<string>();
            var errorCode = errorObject["code"].Value<string>();

            throw new VtigerException(errorCode, errorMessage);
        }
    }
}
