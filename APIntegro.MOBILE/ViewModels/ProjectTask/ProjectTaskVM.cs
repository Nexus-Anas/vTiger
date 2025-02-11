namespace APIntegro.MOBILE.ViewModels.ProjectTask;

public record ProjectTaskPostVM
(
    string projecttaskname,
    string projecttasktype,
    string projecttaskpriority,
    string projectid,
    string assigned_user_id,
    string projecttaskprogress,
    string projecttaskhours,
    string startdate,
    string enddate,
    string projecttaskstatus,
    string? description
);


public record ProjectTaskPutVM
(
    string projecttaskname,
    string projecttasktype,
    string projecttaskpriority,
    string projectid,
    string assigned_user_id,
    string projecttaskprogress,
    string projecttaskhours,
    string startdate,
    string enddate,
    string projecttaskstatus,
    string? description,
    string id
);