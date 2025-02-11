using APIntegro.Domain.Entities;
using APIntegro.WEB.ViewModels.ProjectTask;

namespace APIntegro.WEB.Interfaces;

public interface IProjectTaskService
{
    Task<ProjectTask?> FindProjectTask(string projectTaskId);
    Task<IEnumerable<ProjectTask>?> FindAllProjectTasks();
    Task<int> ProjectTasksCount();
    Task<Dictionary<string, double>?> ProjectTasksStatesChart();
    Task<(bool success, string message)> CreateProjectTask(ProjectTaskPostVM ProjectTask);
    Task<(bool success, string message)> UpdateProjectTask(ProjectTaskPutVM ProjectTask);
    Task<(bool success, string message)> DeleteProjectTask(string projectId);
}