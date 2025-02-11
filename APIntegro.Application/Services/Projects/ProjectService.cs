using APIntegro.Application.IHandlers;
using APIntegro.Application.Interfaces;
using APIntegro.Application.Services.Authentication;
using APIntegro.Domain.Entities;

namespace APIntegro.Application.Services.Projects;

public class ProjectService : IProjectService
{
    private readonly IProjectHandler _projectHandler;
    private readonly IProjectTaskHandler _projectTaskHandler;
    private readonly ITrelloService _trelloService;
    private readonly ISMSService _smsService;
    private readonly LoginSession _session;

    public ProjectService
    (
        IProjectHandler projectHandler,
        IProjectTaskHandler projectTaskHandler,
        ITrelloService trelloService,
        ISMSService smsService,
        LoginSession session
    )
    {
        _projectHandler = projectHandler;
        _projectTaskHandler = projectTaskHandler;
        _trelloService = trelloService;
        _smsService = smsService;
        _session = session;
    }




    public Task<Project> FindProject(string projectId) =>
            _projectHandler.GetProject(_session.User.sessionName, projectId);


    public Task<IEnumerable<Project>> FindAllProjects() =>
        _projectHandler.GetAllProjects(_session.User.sessionName);


    public async Task<int> ProjectsCount() =>
            (await _projectHandler.GetAllProjects(_session.User.sessionName)).Count();


    public async Task<Dictionary<string, double>> ProjectsStatesChart() =>
            (await _projectHandler.GetAllProjects(_session.User.sessionName))
            .GroupBy(p => p.projectstatus)
            .ToDictionary(g => g.Key, g => (double)g.Count());


    public async Task<Project> CreateProject(Project project)
    {
        var createdProject = await _projectHandler.PostProject(_session.User.sessionName, project);

        if (createdProject is not null)
            await _trelloService.CreateBoard(createdProject.projectname);

        return createdProject;
    }


    public async Task<Project> UpdateProject(Project request)
    {
        var project = await FindProject(request.id);
        var updatedProject = await _projectHandler.PutProject(_session.User.sessionName, request);
        await AlertForNewChanges(project, updatedProject);

        if (project is not null && updatedProject is not null && project.projectname != updatedProject.projectname)
            await _trelloService.UpdateBoardName(project.projectname, updatedProject.projectname);

        return updatedProject;
    }


    public async Task<bool> DeleteProject(string projectId)
    {
        await DeleteProjectTasks(projectId);
        await _trelloService.DeleteBoard(projectId);

        bool success = await _projectHandler.RemoveProject(_session.User.sessionName, projectId);

        return success;
    }


    private static byte ParseProgress(string progress) =>
            byte.TryParse(progress?.TrimEnd('%'), out var result) ? result : (byte)0;


    private async Task AlertForNewChanges(Project project, Project updatedProject)
    {
        if (project is null || updatedProject is null) return;

        var oldProgress = ParseProgress(project.progress);
        var newProgress = ParseProgress(updatedProject.progress);

        var progressMessage = newProgress > oldProgress ? $"The project progress has increased from {oldProgress}% to {newProgress}%." : null;
        var statusMessage = project.projectstatus != updatedProject.projectstatus ? $"The project status has changed from {project.projectstatus} to {updatedProject.projectstatus}." : null;
        var priorityMessage = project.projectpriority != updatedProject.projectpriority ? $"The project priority has moved from {project.projectpriority} to {updatedProject.projectpriority}." : null;

        if (progressMessage is null && statusMessage is null && priorityMessage is null) return;

        await _smsService.SendAsync(
            message: $"Project {updatedProject.projectname} update status:\n{progressMessage}\n{statusMessage}\n{priorityMessage}",
            to: "+212639757824"
        );
    }


    private async Task DeleteProjectTasks(string projectId)
    {
        var projectTasks = (await _projectTaskHandler.GetAllProjectTasks(_session.User.sessionName)).Where(p => p.projectid == projectId).ToList();

        if (!projectTasks.Any()) return;

        projectTasks.ForEach(async task => await _projectTaskHandler.RemoveProjectTask(_session.User.sessionName, task.id));
    }
}