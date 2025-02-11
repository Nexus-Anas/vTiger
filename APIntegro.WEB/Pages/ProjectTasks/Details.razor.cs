using APIntegro.Domain.Entities;
using APIntegro.WEB.ViewModels.ProjectTask;
using MudBlazor;

namespace APIntegro.WEB.Pages.ProjectTasks;

public partial class Details
{
    private async Task<IEnumerable<Project>?> LoadProjects()
        => await _projectService.FindAllProjects();


    private async Task Update()
    {
        var request = UpdateProjectRequest();
        var (success, message) = await _projectTaskService.UpdateProjectTask(request);

        _isLoading = false;
        BtnIcon = Icons.Material.Filled.Sync;

        Snackbar.Add(message, success ? Severity.Success : Severity.Error);
    }


    private ProjectTaskPutVM UpdateProjectRequest()
    {
        BtnIcon = string.Empty;
        _isLoading = true;

        var projectTaskPutVM = _mapper.Map<ProjectTaskPutVM>(ProjectTask);
        return projectTaskPutVM;
    }
}