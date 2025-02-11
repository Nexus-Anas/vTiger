using APIntegro.WEB.ViewModels.Project;
using MudBlazor;

namespace APIntegro.WEB.Pages.Projects;

public partial class Details
{
    private async Task Update()
    {
        var request = UpdateProjectRequest();
        var (success, message) = await _projectService.UpdateProject(request);

        _isLoading = false;
        BtnIcon = Icons.Material.Filled.Sync;

        Snackbar.Add(message, success ? Severity.Success : Severity.Error);
    }


    private ProjectPutVM UpdateProjectRequest()
    {
        BtnIcon = string.Empty;
        _isLoading = true;

        var projectPutVM = _mapper.Map<ProjectPutVM>(Project);
        return projectPutVM;
    }
}