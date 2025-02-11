using APIntegro.Domain.Entities;

namespace APIntegro.Application.Interfaces;

public interface IProjectService
{
    Task<Project> FindProject(string projectId);
    Task<IEnumerable<Project>> FindAllProjects();
    Task<int> ProjectsCount();
    Task<Dictionary<string, double>> ProjectsStatesChart();
    Task<Project> CreateProject(Project project);
    Task<Project> UpdateProject(Project project);
    Task<bool> DeleteProject(string projectId);
}