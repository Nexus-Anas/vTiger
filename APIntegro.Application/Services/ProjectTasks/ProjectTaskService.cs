using APIntegro.Application.IHandlers;
using APIntegro.Application.Interfaces;
using APIntegro.Application.Services.Authentication;
using APIntegro.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TrelloDotNet;
using TrelloDotNet.Model;

namespace APIntegro.Application.Services.ProjectTasks;

public class ProjectTaskService : IProjectTaskService
{
    private readonly IProjectTaskHandler _projectTaskHandler;
    private readonly IProjectService _projectService;
    private readonly ITrelloService _trelloService;
    private readonly ISMSService _smsService;
    private readonly LoginSession _session;
    private readonly ILogger<ProjectTaskService> _logger;
    private readonly IMailingHandler _mailingHandler;

    public ProjectTaskService
    (
        IProjectTaskHandler projectTaskHandler,
        IProjectService projectService,
        ITrelloService trelloService,
        ISMSService smsService, 
        LoginSession session, 
        ILogger<ProjectTaskService> logger, 
        IMailingHandler mailingHandler
    )
    {
        _projectTaskHandler = projectTaskHandler;
        _projectService = projectService;
        _trelloService = trelloService;
        _smsService = smsService;
        _session = session;
        _logger = logger;
        _mailingHandler = mailingHandler;
    }


    public async Task<ProjectTask> FindProjectTask(string projectTaskId) =>
            await _projectTaskHandler.GetProjectTask(_session.User.sessionName, projectTaskId);


    public async Task<IEnumerable<ProjectTask>> FindAllProjectTasks() =>
        await _projectTaskHandler.GetAllProjectTasks(_session.User.sessionName);


    public async Task<int> ProjectTasksCount() =>
            (await _projectTaskHandler.GetAllProjectTasks(_session.User.sessionName)).Count();


    public async Task<Dictionary<string, double>> ProjectTasksStatesChart() =>
            (await _projectTaskHandler.GetAllProjectTasks(_session.User.sessionName))
            .GroupBy(p => p.projecttaskstatus)
            .ToDictionary(g => g.Key, g => (double)g.Count());


    public async Task<ProjectTask> CreateProjectTask(ProjectTask projectTask)
    {
        var createdProjectTask = await _projectTaskHandler.PostProjectTask(_session.User.sessionName, projectTask);

        if (createdProjectTask is not null)
        {
            var project = await _projectService.FindProject(createdProjectTask.projectid);
            var parentBoard = (await _trelloService.GetBoards()).FirstOrDefault(b => b.Name.Equals(project.projectname));
            var parentList = (await _trelloService.GetBoardLists(parentBoard))
                             .FirstOrDefault(l => l.Name.Equals(createdProjectTask.projecttaskstatus));

            if (parentList is not null) await _trelloService.CreateCard(createdProjectTask, parentList);
        }

        return createdProjectTask;
    }


    public async Task<ProjectTask> UpdateProjectTask(ProjectTask projectTask)
    {
        var existingTask = await FindProjectTask(projectTask.id);
        byte oldProgress = ParseProgress((await FindProjectTask(projectTask.id))?.projecttaskprogress);
        var updatedProjectTask = await _projectTaskHandler.PutProjectTask(_session.User.sessionName, projectTask);
        byte newProgress = ParseProgress(updatedProjectTask?.projecttaskprogress);

        if (updatedProjectTask is not null)
        {
            if (newProgress > oldProgress)
                await NotifyTaskProgressUpdate(updatedProjectTask, oldProgress, newProgress);

            if (newProgress == 100)
                await NotifyTaskCompletion(updatedProjectTask);

            await _trelloService.UpdateCard(existingTask, updatedProjectTask);
        }

        return updatedProjectTask;
    }


    public async Task<bool> DeleteProjectTask(string projectTaskId)
    {
        await _trelloService.DeleteCard(projectTaskId);

        bool success = await _projectTaskHandler.RemoveProjectTask(_session.User.sessionName, projectTaskId);

        return success;
    }
            

    private async Task NotifyTaskProgressUpdate(ProjectTask updatedProjectTask, byte oldProgress, byte newProgress)
    {
        var message = $"Project Task \"{updatedProjectTask.projecttaskname}\" progress has increased from {oldProgress}% to {newProgress}%";
        await _smsService.SendAsync(message, "+212639757824");

        var email = new Email("mohcinelaaroubi@gmail.com", "Project Task progress status", message);
        await _mailingHandler.SendEmail(email);
    }


    private async Task NotifyTaskCompletion(ProjectTask updatedProjectTask)
    {
        var message = $"Project Task \"{updatedProjectTask.projecttaskname}\" is completed successfully";
        await _smsService.SendAsync(message, "+212639757824");

        var email = new Email("mohcinelaaroubi@gmail.com", "A new task has been completed", $"Hi,\n{updatedProjectTask.projecttaskname} is completed successfully!");
        await _mailingHandler.SendEmail(email);
    }


    private static byte ParseProgress(string progress)
        => byte.TryParse(progress?.TrimEnd('%'), out byte result) ? result : (byte)0;
}