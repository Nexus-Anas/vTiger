using APIntegro.Application.DTOs.ProjectMilestone;

namespace APIntegro.Application.Services.ProjectMilestones;

public record UpdateProjectMilestoneRequest(
        string SessionName,
        UpdateProjectMilestoneDto ProjectMilestoneDto
    );
