using APIntegro.Domain.Entities;
using APIntegro.WEB.ViewModels.Project;
using MudBlazor;

namespace APIntegro.WEB.Pages.Projects;

public partial class Create
{
    private async Task Submit()
    {
        var request = CreateProjectRequest();
        var (success, message) = await _projectService.CreateProject(request);

        _isLoading = false;
        BtnIcon = Icons.Material.Filled.FileDownloadDone;

        Snackbar.Add(message, success ? Severity.Info : Severity.Error);

        if (success) ClearFields();
    }


    private ProjectPostVM CreateProjectRequest()
    {
        BtnIcon = string.Empty;
        _isLoading = true;

        Project.assigned_user_id = Session.User.userId;
        var projectPostVM = _mapper.Map<ProjectPostVM>(Project);
        return projectPostVM;
    }


    private void ClearFields()
    {
        StartDate = TargetEndDate = ActualEndDate = null;
        TargetBudget = 0.00m;
        Project = new();
    }
}