using APIntegro.WEB.Data;
using APIntegro.Domain.Entities;
using APIntegro.WEB.Interfaces;
using APIntegro.WEB.ViewModels.Organization;

namespace APIntegro.WEB.Services;

public class OrganizationService : IOrganizationService
{
    private readonly HttpClient _http;
    private readonly string? _baseUrl;
    private readonly string _controller = "Organizations";


    public OrganizationService(HttpClient http, IConfiguration configuration)
    {
        _http = http;
        _baseUrl = configuration.GetValue<string>("ApiSettings:APIntegroUrl");
    }



    public async Task<Organization?> FindOrganization(string organizationId)
    {
        using var response = await _http.GetAsync($"{_baseUrl}{_controller}/GetById?organizationId={organizationId}").ConfigureAwait(false);
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<Organization>() : null;
    }

    public async Task<IEnumerable<Organization>?> FindAllOrganizations()
    {
        using var response = await _http.GetAsync($"{_baseUrl}{_controller}/GetAll");
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<IEnumerable<Organization>>() : null;
    }

    public async Task<int> OrganizationsCount()
    {
        using var response = await _http.GetAsync($"{_baseUrl}{_controller}/Count");
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<int>() : 0;
    }

    public async Task<(bool success, string message)> CreateOrganization(OrganizationPostVM organization)
    {
        try
        {
            using var response = await _http.PostAsJsonAsync($"{_baseUrl}{_controller}/Create", organization);

            return response.IsSuccessStatusCode
                ? (true, "Organization created with success")
                : (false, await GetErrorMessage(response, "An error occurred while creating the organization."));
        }
        catch (Exception ex)
        {
            return (false, "An error occurred: " + ex.Message);
        }
    }

    public async Task<(bool success, string message)> UpdateOrganization(OrganizationPutVM organization)
    {
        try
        {
            using var response = await _http.PutAsJsonAsync($"{_baseUrl}{_controller}/Update", organization);
            return response.IsSuccessStatusCode
                ? (true, "Organization updated successfully")
                : (false, await GetErrorMessage(response, "An error occurred while updating the organization."));
        }
        catch (Exception ex)
        {
            return (false, "An error occurred: " + ex.Message);
        }
    }

    public async Task<(bool success, string message)> DeleteOrganization(string organizationId)
    {
        try
        {
            using var response = await _http.DeleteAsync($"{_baseUrl}{_controller}/Delete?organizationId={organizationId}");
            return response.IsSuccessStatusCode
                ? (true, "Organization deleted successfully")
                : (false, await GetErrorMessage(response, "An error occurred while deleting the organization."));
        }
        catch (Exception ex)
        {
            return (false, "An error occurred: " + ex.Message);
        }
    }




    private async Task<string> GetErrorMessage(HttpResponseMessage response, string errorMessage)
    {
        try
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return errorResponse?.Title ?? errorMessage;
        }
        catch { return "An error occurred while processing the response."; }
    }
}