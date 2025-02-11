using APIntegro.Domain.Entities;
using APIntegro.MOBILE.ViewModels.Project;

namespace APIntegro.MOBILE.Interfaces;

public interface IProjectService
{
    Task<Project?> FindProject(string projectId);
    Task<IEnumerable<Project>?> FindAllProjects();
    Task<int> ProjectsCount();
    Task<Dictionary<string, double>?> ProjectsStatesChart();
    Task<(bool success, string message)> CreateProject(ProjectPostVM Project);
    Task<(bool success, string message)> UpdateProject(ProjectPutVM Project);
    Task<(bool success, string message)> DeleteProject(string projectId);
}