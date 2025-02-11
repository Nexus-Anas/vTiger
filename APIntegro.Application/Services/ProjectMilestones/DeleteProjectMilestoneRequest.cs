namespace APIntegro.Application.Services.ProjectMilestones;

public record DeleteProjectMilestoneRequest(
        string SessionName,
        string ProjectMilestoneId
    );
