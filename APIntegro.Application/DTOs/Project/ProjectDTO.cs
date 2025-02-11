namespace APIntegro.Application.DTOs.Project;

public record ProjectPostDTO
(
    string projectname,
    string? startdate,
    string? targetenddate,
    string? actualenddate,
    string? projectstatus,
    string? projecttype,
    string? linktoaccountscontacts,
    string assigned_user_id,
    string? project_no,
    string? targetbudget,
    string? projecturl,
    string? projectpriority,
    string? progress,
    string? createdtime,
    string? description,
    string? source,
    string? starred,
    string? tags,
    string? label
);


public record ProjectPutDTO
(
    string projectname,
    string? startdate,
    string? targetenddate,
    string? actualenddate,
    string? projectstatus,
    string? projecttype,
    string? linktoaccountscontacts,
    string assigned_user_id,
    string? project_no,
    string? targetbudget,
    string? projecturl,
    string? projectpriority,
    string? progress,
    string? createdtime,
    string? modifiedtime,
    string? modifiedby,
    string? description,
    string? source,
    string? starred,
    string? tags,
    string? id,
    string? label
);