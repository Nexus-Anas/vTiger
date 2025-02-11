using System.ComponentModel.DataAnnotations;

namespace APIntegro.Domain.Entities;

public class ProjectMilestone
{
    [Required(ErrorMessage = "Organization name is required.")]
    public string projectmilestonename { get; set; }

    [Required(ErrorMessage = "Organization name is required.")]
    public string projectmilestonedate { get; set; }
    public string projectid { get; set; }
    public string? projectmilestonetype { get; set; }
    public string? assigned_user_id { get; set; }
    public string? projectmilestone_no { get; set; }
    public string? createdtime { get; set; }
    public string? modifiedtime { get; set; }
    public string? modifiedby { get; set; }
    public string? description { get; set; }
    public string? source { get; set; }
    public string? starred { get; set; } 
    public string? tags {  get; set; }
    public string? id { get; set; }
}
