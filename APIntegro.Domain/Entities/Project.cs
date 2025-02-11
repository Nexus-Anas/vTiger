using System.ComponentModel.DataAnnotations;

namespace APIntegro.Domain.Entities;
public class Project
{
    [Required(ErrorMessage = "The project name is required.")]
    public string projectname { get; set; }

    [Required(ErrorMessage = "The project start date is required.")]
    public string startdate { get; set; }

    [Required(ErrorMessage = "The project target end date is required.")]
    public string targetenddate { get; set; }

    [Required(ErrorMessage = "The project actual end date is required.")]
    public string actualenddate { get; set; }

    [Required(ErrorMessage = "The project status is required.")]
    public string projectstatus { get; set; }

    [Required(ErrorMessage = "The project type is required.")]
    public string projecttype { get; set; }
    public string? linktoaccountscontacts { get; set; }
    public string assigned_user_id { get; set; }
    public string? project_no { get; set; }

    [Required(ErrorMessage = "The target budget is required.")]
    public string targetbudget { get; set; } = "0.00";
    public string? projecturl { get; set; }

    [Required(ErrorMessage = "The project priority is required.")]
    public string projectpriority { get; set; }

    [Required(ErrorMessage = "The project progress is required.")]
    public string progress { get; set; } = "0%";
    public string? createdtime { get; set; }
    public string? modifiedtime { get; set; }
    public string? modifiedby { get; set; }
    public string? description { get; set; }
    public string? source { get; set; }
    public string? starred { get; set; }
    public string? tags { get; set; }
    public string? isconvertedfrompotential { get; set; }
    public string? potentialid { get; set; }
    public string? id { get; set; }
    public string? label { get; set; }
}
