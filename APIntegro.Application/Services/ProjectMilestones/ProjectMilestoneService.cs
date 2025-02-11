using APIntegro.Application.IHandlers;
using APIntegro.Application.Interfaces;
using APIntegro.Domain.Entities;
using APIntegro.Application.Services.Authentication;
using APIntegro.Application.DTOs.ProjectMilestone;

namespace APIntegro.Application.Services.ProjectMilestones;

public class ProjectMilestoneService : IProjectMilestoneService
{
    private readonly IProjectMilestoneHandler _projectMilestoneHandler;
    private readonly LoginSession _session;

    public ProjectMilestoneService(IProjectMilestoneHandler projectMilestoneHandler, LoginSession session)
    {
        _projectMilestoneHandler = projectMilestoneHandler;
        _session = session;
    }

    public async Task<ProjectMilestone> CreateProjectMilestone(CreateProjectMilestoneDto projectMilestone)
    {
        return await _projectMilestoneHandler.PostProjectMilestone(_session.User.sessionName, projectMilestone);
    }

    public async Task<bool> DeleteProjectMilestone(string projectMilestoneId)
    {
        return await _projectMilestoneHandler.RemoveProjectMilestone(_session.User.sessionName, projectMilestoneId);
    }

    public async Task<IEnumerable<ProjectMilestone>> GetAllProjectMilestones()
    {
        return await _projectMilestoneHandler.GetAllProjectMilestones(_session.User.sessionName);
    }

    public async Task<ProjectMilestone> GetProjectMilestone(string projectMilestoneId)
    {
        return await _projectMilestoneHandler.GetProjectMilestone(_session.User.sessionName, projectMilestoneId);
    }

    public async Task<ProjectMilestone> UpdateProjectMilestone(UpdateProjectMilestoneDto projectMilestone)
    {
        return await _projectMilestoneHandler.PutProjectMilestone(_session.User.sessionName, projectMilestone);
    }
}
