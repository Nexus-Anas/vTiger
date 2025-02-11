using APIntegro.Domain.Entities;

namespace APIntegro.Application.Interfaces;

public interface IProjectTaskService
{
    Task<ProjectTask> FindProjectTask(string projectTaskId);
    Task<IEnumerable<ProjectTask>> FindAllProjectTasks();
    Task<int> ProjectTasksCount();
    Task<Dictionary<string, double>> ProjectTasksStatesChart();
    Task<ProjectTask> CreateProjectTask(ProjectTask projectTask);
    Task<ProjectTask> UpdateProjectTask(ProjectTask projectTask);
    Task<bool> DeleteProjectTask(string projectTaskId);
}