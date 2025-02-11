using APIntegro.MOBILE.Data;
using APIntegro.Domain.Entities;
using APIntegro.MOBILE.Interfaces;
using APIntegro.MOBILE.ViewModels.Project;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace APIntegro.MOBILE.Services;

public class ProjectService : IProjectService
{
    private readonly HttpClient _http;
    private readonly string? _baseUrl;
    private readonly string _controller = "Project";

    public ProjectService(HttpClient http, IConfiguration configuration)
    {
        _http = http;
        _baseUrl = "https://funmintski42.conveyor.cloud/api/";
    }




    public async Task<Project?> FindProject(string projectId)
    {
        using var response = await _http.GetAsync($"{_baseUrl}{_controller}/Find?projectId={projectId}").ConfigureAwait(false);
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<Project>() : null;
    }

    public async Task<IEnumerable<Project>?> FindAllProjects()
    {
        using var response = await _http.GetAsync($"{_baseUrl}{_controller}/FindAll");
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<IEnumerable<Project>>() : null;
    }

    public async Task<int> ProjectsCount()
    {
        using var response = await _http.GetAsync($"{_baseUrl}{_controller}/Count");
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<int>() : 0;
    }

    public async Task<Dictionary<string, double>?> ProjectsStatesChart()
    {
        using var response = await _http.GetAsync($"{_baseUrl}{_controller}/StatesChart");
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<Dictionary<string, double>>() : null;
    }

    public async Task<(bool success, string message)> CreateProject(ProjectPostVM project)
    {
        try
        {
            using var response = await _http.PostAsJsonAsync($"{_baseUrl}{_controller}/Create", project);

            return response.IsSuccessStatusCode
                ? (true, "Project created with success")
                : (false, await GetErrorMessage(response, "An error occurred while creating the project."));
        }
        catch (Exception ex)
        {
            return (false, "An error occurred: " + ex.Message);
        }
    }

    public async Task<(bool success, string message)> UpdateProject(ProjectPutVM project)
    {
        try
        {
            using var response = await _http.PutAsJsonAsync($"{_baseUrl}{_controller}/Update", project);
            return response.IsSuccessStatusCode
                ? (true, "Project updated successfully")
                : (false, await GetErrorMessage(response, "An error occurred while updating the project."));
        }
        catch (Exception ex)
        {
            return (false, "An error occurred: " + ex.Message);
        }
    }

    public async Task<(bool success, string message)> DeleteProject(string projectId)
    {
        try
        {
            using var response = await _http.DeleteAsync($"{_baseUrl}{_controller}/Delete?projectId={projectId}");
            return response.IsSuccessStatusCode
                ? (true, "Project deleted successfully")
                : (false, await GetErrorMessage(response, "An error occurred while deleting the project."));
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