using APIntegro.Application.Services.Projects;
using APIntegro.Domain.Entities;

namespace APIntegro.Application.IHandlers;

public interface IProjectHandler
{
    Task<Project> GetProject(string sessionName, string projectId);
    Task<IEnumerable<Project>> GetAllProjects(string sessionName);
    Task<Project> PostProject(string sessionName, Project project);
    Task<Project> PutProject(string sessionName, Project project);
    Task<bool> RemoveProject(string sessionName, string projectId);
}