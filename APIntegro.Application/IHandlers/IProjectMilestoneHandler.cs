using APIntegro.Application.DTOs.Organization;
using APIntegro.Application.DTOs.ProjectMilestone;
using APIntegro.Application.Services.ProjectMilestones;
using APIntegro.Domain.Entities;

namespace APIntegro.Application.IHandlers;

public interface IProjectMilestoneHandler
{
    Task<ProjectMilestone> GetProjectMilestone(string sessionName, string projectMilestoneId);
    Task<IEnumerable<ProjectMilestone>> GetAllProjectMilestones(string sessionName);
    Task<ProjectMilestone> PostProjectMilestone(string sessionName, CreateProjectMilestoneDto organization);
    Task<ProjectMilestone> PutProjectMilestone(string sessionName, UpdateProjectMilestoneDto organization);
    Task<bool> RemoveProjectMilestone(string sessionName, string projectMilestoneId);
}
