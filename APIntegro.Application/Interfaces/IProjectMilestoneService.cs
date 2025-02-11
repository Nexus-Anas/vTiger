using APIntegro.Application.DTOs.ProjectMilestone;
using APIntegro.Domain.Entities;

namespace APIntegro.Application.Interfaces;

public interface IProjectMilestoneService
{
    Task<ProjectMilestone> GetProjectMilestone(string projectMilestone);
    Task<IEnumerable<ProjectMilestone>> GetAllProjectMilestones();
    Task<ProjectMilestone> CreateProjectMilestone(CreateProjectMilestoneDto projectMilestone);
    Task<ProjectMilestone> UpdateProjectMilestone(UpdateProjectMilestoneDto projectMilestone);
    Task<bool> DeleteProjectMilestone(string projectMilestone);
}
