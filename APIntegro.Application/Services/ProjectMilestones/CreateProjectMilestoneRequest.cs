using APIntegro.Application.DTOs.ProjectMilestone;

namespace APIntegro.Application.Services.ProjectMilestones;

public record CreateProjectMilestoneRequest(
        string SessionName,
        CreateProjectMilestoneDto ProjectMilestoneDto
    );

