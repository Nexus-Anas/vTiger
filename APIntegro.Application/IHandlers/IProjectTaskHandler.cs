using APIntegro.Application.Services.ProjectTasks;
using APIntegro.Domain.Entities;

namespace APIntegro.Application.IHandlers;

public interface IProjectTaskHandler
{
    Task<ProjectTask> GetProjectTask(string sessionName, string projectTaskId);
    Task<IEnumerable<ProjectTask>> GetAllProjectTasks(string sessionName);
    Task<ProjectTask> PostProjectTask(string SessionName, ProjectTask ProjectTask);
    Task<ProjectTask> PutProjectTask(string SessionName, ProjectTask ProjectTask);
    Task<bool> RemoveProjectTask(string sessionName, string projectTaskId);
}