using APIntegro.MOBILE.Components;
using APIntegro.Domain.Entities;
using MudBlazor;

namespace APIntegro.MOBILE.Pages.ProjectTasks;

public partial class ProjectTaskList
{
    private async Task<IEnumerable<ProjectTask>?> LoadProjectTasks()
        => await _projectTaskService.FindAllProjectTasks();


    private Func<ProjectTask, bool> quickFilter => x =>
    {
        if (string.IsNullOrWhiteSpace(_searchQuery))
            return true;

        var searchTerms = $"{x.projecttaskname} {x.startdate} {x.enddate} {x.projecttaskpriority} {x.projecttaskprogress} {x.projecttaskhours} {x.projecttaskstatus}";
        return searchTerms.Contains(_searchQuery, StringComparison.OrdinalIgnoreCase);
    };


    private async Task Delete(ProjectTask projectTask)
    {
        var result = await ConfirmOperation($"Are you sure you want to delete {projectTask.projecttaskname}?");

        if (result.Canceled) return;


        var (success, message) = await _projectTaskService.DeleteProjectTask(projectTask.id);

        if (success)
        {
            Snackbar.Add(message, Severity.Info);
            ProjectTasks = await LoadProjectTasks();
            _noProjectFound = !ProjectTasks.Any();
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
            byte p when p > 60 && p <= 90 => MudBlazor.Color.Tertiary,
            _ => MudBlazor.Color.Success
        };
    }


    private async Task<DialogResult> ConfirmOperation(string dialogueMessage)
    {
        var options = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, ClassBackground = "my-custom-class" };
        var @params = new DialogParameters<RemoveDialog> { { x => x.Message, dialogueMessage } };
        var dialog = await DialogService.ShowAsync<RemoveDialog>("Delete Project", @params, options);
        return await dialog.Result;
    }
}