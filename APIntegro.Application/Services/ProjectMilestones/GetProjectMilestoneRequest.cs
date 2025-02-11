namespace APIntegro.Application.Services.ProjectMilestones;

public record  GetProjectMilestoneRequest(
        string SessionName,
        string ProjectMilestoneId
    );
