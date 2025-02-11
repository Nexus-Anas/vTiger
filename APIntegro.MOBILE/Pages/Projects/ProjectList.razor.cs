using APIntegro.MOBILE.Components;
using APIntegro.Domain.Entities;
using MudBlazor;

namespace APIntegro.MOBILE.Pages.Projects;

public partial class ProjectList
{
    private async Task<IEnumerable<Project>?> LoadProjects()
        => await _projectService.FindAllProjects();


    private Func<Project, bool> quickFilter => p =>
    {
        if (string.IsNullOrWhiteSpace(_searchQuery))
            return true;

        var searchTerms = $"{p.projectname} {p.startdate} {p.targetenddate} {p.actualenddate} {p.projectstatus} {p.targetbudget} {p.projectpriority} {p.progress}";
        return searchTerms.Contains(_searchQuery, StringComparison.OrdinalIgnoreCase);
    };


    private async Task Delete(Project project)
    {
        var result = await ConfirmOperation($"Are you sure you want to delete {project.projectname}?");

        if (result.Canceled) return;


        var (success, message) = await _projectService.DeleteProject(project.id);

        if (success)
        {
            Snackbar.Add(message, Severity.Info);
            Projects = await LoadProjects();
            _noProjectFound = !Projects.Any();
        }
        else
            Snackbar.Add(message, Severity.Error);
    }


    private MudBlazor.Color GetProgressColor(string value)
    {
        byte progress = Convert.ToByte(value.TrimEnd('%'));

        return progress switch
        {
            byte p when p >= 0 && p <= 30 => MudBlazor.Color.Secondary,
            byte p when p > 30 && p <= 60 => MudBlazor.Color.Warning,
            byte p when p > 60 && p <= 90 => MudBlazor.Color.Info,
            _ => MudBlazor.Color.Success
        };
    }


    private async Task<DialogResult> ConfirmOperation(string dialogueMessage)
    {
        var options = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, ClassBackground = "my-custom-class" };
        var @params = new DialogParameters<RemoveDialog> { { x => x.Message, dialogueMessage } };
        var dialog = await DialogService.ShowAsync<RemoveDialog>("Remove Project", @params, options);
        return await dialog.Result;
    }
}