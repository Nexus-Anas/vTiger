namespace APIntegro.Application.DTOs.ProjectMilestone;

public record CreateProjectMilestoneDto(
    string? projectmilestonename,
    string? projectmilestonedate,
    string projectid,
    string? projectmilestonetype,
    string assigned_user_id,
    string? projectmilestone_no,
    string? description,
    string? source,
    string? starred,
    string? tags
);

