namespace APIntegro.Application.DTOs.ProjectMilestone;

public record UpdateProjectMilestoneDto(
    string projectmilestonename,
    string? projectmilestonedate,
    string projectid,
    string? projectmilestonetype,
    string assigned_user_id,
    string? projectmilestone_no,
    string? description,
    string? source,
    string? starred,
    string? tags,
    string? id
);


