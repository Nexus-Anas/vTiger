using APIntegro.Domain.Entities;
using APIntegro.MOBILE.ViewModels.ProjectTask;
using MudBlazor;

namespace APIntegro.MOBILE.Pages.ProjectTasks;

public partial class Create
{
    private async Task<IEnumerable<Project>?> LoadProjects()
        => await _projectService.FindAllProjects();


    private async Task Submit()
    {
        var request = CreateProjectTaskRequest();
        var (success, message) = await _projectTaskService.CreateProjectTask(request);

        _isLoading = false;
        BtnIcon = Icons.Material.Filled.FileDownloadDone;

        Snackbar.Add(message, success ? Severity.Info : Severity.Error);

        if (success) ClearFields();
    }


    private ProjectTaskPostVM CreateProjectTaskRequest()
    {
        BtnIcon = string.Empty;
        _isLoading = true;

        ProjectTask.assigned_user_id = Session.User.userId;
        var projectTaskPostVM = _mapper.Map<ProjectTaskPostVM>(ProjectTask);
        return projectTaskPostVM;
    }


    private void ClearFields()
    {
        StartDate = EndDate = null;
        TaskHours = 0;
        ProjectTask = new();
    }
}