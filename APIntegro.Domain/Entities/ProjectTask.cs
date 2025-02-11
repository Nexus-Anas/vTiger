using System.ComponentModel.DataAnnotations;

namespace APIntegro.Domain.Entities;
public class ProjectTask
{
    [Required(ErrorMessage = "Task name is required.")]
    public string projecttaskname { get; set; }

    [Required(ErrorMessage = "Task type is required.")]
    public string projecttasktype { get; set; }

    [Required(ErrorMessage = "Task priority is required.")]
    public string projecttaskpriority { get; set; }

    public string projectid { get; set; }
    public string assigned_user_id { get; set; }
    public string? projecttasknumber { get; set; }
    public string? projecttask_no { get; set; }

    [Required(ErrorMessage = "Task progress is required.")]
    public string projecttaskprogress { get; set; }
    public string? projecttaskhours { get; set; }

    [Required(ErrorMessage = "Task start date is required.")]
    public string startdate { get; set; }

    [Required(ErrorMessage = "Task end date is required.")]
    public string enddate { get; set; }
    public string? createdtime { get; set; }
    public string? modifiedtime { get; set; }
    public string? modifiedby { get; set; }
    public string? description { get; set; }
    public string? source { get; set; }
    public string? starred { get; set; }
    public string? tags { get; set; }

    [Required(ErrorMessage = "Task status is required.")]
    public string projecttaskstatus { get; set; }
    public string? id { get; set; }
    public string? label { get; set; }
}