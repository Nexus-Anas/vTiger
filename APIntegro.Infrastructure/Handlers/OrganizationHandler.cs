using APIntegro.Application.Common.Errors;
using APIntegro.Application.DTOs.Organization;
using APIntegro.Application.IHandlers;
using APIntegro.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APIntegro.Infrastructure.Handlers;

public class OrganizationHandler : IOrganizationHandler
{
    private readonly IApiHandler _apiHandler;

    public OrganizationHandler(IApiHandler apiHandler)
    {
        _apiHandler = apiHandler;
    }
    public async Task<IEnumerable<Organization>> GetAllOrganizations(string sessionName)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "query" },
            { "sessionName", sessionName },
            { "query", "SELECT * FROM Accounts;"}
        };

        var response = await _apiHandler.GetApiResponse(urlParams);

        ThrowIfError(response);

        return response["result"].ToObject<IEnumerable<Organization>>();
    }

    public async Task<Organization> GetOrganization(string sessionName, string organizationId)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "retrieve" },
            { "sessionName", sessionName },
            { "id", organizationId }
        };

        var response = await _apiHandler.GetApiResponse(urlParams);

        ThrowIfError(response);

        return response["result"].ToObject<Organization>();
    }

    public async Task<Organization> PostOrganization(string sessionName, CreateOrganizationDto organization)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "create" },
            { "sessionName", sessionName },
            { "element", JsonConvert.SerializeObject(organization) },
            { "elementType", "Accounts" }
        };

        var response = await _apiHandler.PostApiResponse(urlParams);

        ThrowIfError(response);

        return response["result"].ToObject<Organization>();
    }

    public async Task<Organization> PutOrganization(string sessionName, UpdateOrganizationDto organization)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "update" },
            { "sessionName", sessionName },
            { "element", JsonConvert.SerializeObject(organization) },
            { "elementType", "Accounts" }
        };

        var response = await _apiHandler.PostApiResponse(urlParams);

        ThrowIfError(response);

        return response["result"].ToObject<Organization>();
    }

    public async Task<bool> RemoveOrganization(string sessionName, string organizationId)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "delete" },
            { "sessionName", sessionName },
            { "id", organizationId }
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
