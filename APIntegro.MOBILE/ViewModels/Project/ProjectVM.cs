namespace APIntegro.MOBILE.ViewModels.Project;

public record ProjectPostVM
(
    string projectname,
    string startdate,
    string targetenddate,
    string actualenddate,
    string projectstatus,
    string projecttype,
    string assigned_user_id,
    string targetbudget,
    string? projecturl,
    string projectpriority,
    string progress,
    string? description
);


public record ProjectPutVM
(
    string projectname,
    string startdate,
    string targetenddate,
    string actualenddate,
    string projectstatus,
    string projecttype,
    string assigned_user_id,
    string targetbudget,
    string? projecturl,
    string projectpriority,
    string progress,
    string? description,
    string id
);