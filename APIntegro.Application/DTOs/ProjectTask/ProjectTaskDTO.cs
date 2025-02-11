namespace APIntegro.Application.DTOs.ProjectTask;

public record ProjectTaskPostDTO
(
    string projecttaskname,
    string? projecttasktype,
    string? projecttaskpriority,
    string projectid,
    string assigned_user_id,
    string? projecttasknumber,
    string? projecttask_no,
    string? projecttaskprogress,
    string? projecttaskhours,
    string? startdate,
    string? enddate,
    string? createdtime,
    string? description,
    string? source,
    string? starred,
    string? tags,
    string? projecttaskstatus,
    string? label
);



public record ProjectTaskPutDTO
(
    string projecttaskname,
    string? projecttasktype,
    string? projecttaskpriority,
    string projectid,
    string assigned_user_id,
    string? projecttasknumber,
    string? projecttask_no,
    string? projecttaskprogress,
    string? projecttaskhours,
    string? startdate,
    string? enddate,
    string? createdtime,
    string? modifiedtime,
    string? modifiedby,
    string? description,
    string? source,
    string? starred,
    string? tags,
    string? projecttaskstatus,
    string? id,
    string? label
);