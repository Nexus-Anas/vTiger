using APIntegro.WEB.Data;
using APIntegro.Domain.Entities;
using APIntegro.WEB.Interfaces;
using APIntegro.WEB.ViewModels.ProjectTask;

namespace APIntegro.WEB.Services;

public class ProjectTaskService : IProjectTaskService
{
    private readonly HttpClient _http;
    private readonly string? _baseUrl;
    private readonly string _controller = "ProjectTask";

    public ProjectTaskService(HttpClient http, IConfiguration configuration)
    {
        _http = http;
        _baseUrl = configuration.GetValue<string>("ApiSettings:APIntegroUrl");
    }




    public async Task<ProjectTask?> FindProjectTask(string projectTaskId)
    {
        using var response = await _http.GetAsync($"{_baseUrl}{_controller}/Find?projectTaskId={projectTaskId}").ConfigureAwait(false);
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<ProjectTask>() : null;
    }

    public async Task<IEnumerable<ProjectTask>?> FindAllProjectTasks()
    {
        using var response = await _http.GetAsync($"{_baseUrl}{_controller}/FindAll");
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<IEnumerable<ProjectTask>>() : null;
    }

    public async Task<int> ProjectTasksCount()
    {
        using var response = await _http.GetAsync($"{_baseUrl}{_controller}/Count");
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<int>() : 0;
    }

    public async Task<Dictionary<string, double>?> ProjectTasksStatesChart()
    {
        using var response = await _http.GetAsync($"{_baseUrl}{_controller}/StatesChart");
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<Dictionary<string, double>>() : null;
    }

    public async Task<(bool success, string message)> CreateProjectTask(ProjectTaskPostVM project)
    {
        try
        {
            using var response = await _http.PostAsJsonAsync($"{_baseUrl}{_controller}/Create", project);

            return response.IsSuccessStatusCode
                ? (true, "Task created with success")
                : (false, await GetErrorMessage(response, "An error occurred while creating the task."));
        }
        catch (Exception ex)
        {
            return (false, "An error occurred: " + ex.Message);
        }
    }

    public async Task<(bool success, string message)> UpdateProjectTask(ProjectTaskPutVM project)
    {
        try
        {
            using var response = await _http.PutAsJsonAsync($"{_baseUrl}{_controller}/Update", project);
            return response.IsSuccessStatusCode
                ? (true, "Task updated successfully")
                : (false, await GetErrorMessage(response, "An error occurred while updating the task."));
        }
        catch (Exception ex)
        {
            return (false, "An error occurred: " + ex.Message);
        }
    }

    public async Task<(bool success, string message)> DeleteProjectTask(string projectTaskId)
    {
        try
        {
            using var response = await _http.DeleteAsync($"{_baseUrl}{_controller}/Delete?projectTaskId={projectTaskId}");
            return response.IsSuccessStatusCode
                ? (true, "Task deleted successfully")
                : (false, await GetErrorMessage(response, "An error occurred while deleting the task."));
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