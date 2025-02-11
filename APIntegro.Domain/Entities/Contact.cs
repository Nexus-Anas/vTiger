using System.ComponentModel.DataAnnotations;

namespace APIntegro.Domain.Entities;

public class Contact
{
    public string? salutationtype { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    public string firstname { get; set; }
    public string? contactno { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    public string phone { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    public string lastname { get; set; }
    public string? mobile { get; set; }
    public string? accountid { get; set; }
    public string? homephone { get; set; }
    public string? leadsource { get; set; }
    public string? otherphone { get; set; }
    public string? title { get; set; }
    public string? fax { get; set; }

    [Required(ErrorMessage = "Contact department is required")]
    public string department { get; set; }

    [Required(ErrorMessage = "Birth day is required.")]
    public string birthday { get; set; }

    [EmailAddress(ErrorMessage = "Please enter a valid email address."), Required(ErrorMessage = "Email address is required.")]
    public string email { get; set; }
    public string? contactid { get; set; }
    public string? assistant { get; set; }
    public string? secondaryemail { get; set; }
    public string? assistantphone { get; set; }
    public string? dontcall { get; set; }
    public string? emailoptout { get; set; }
    public string? assigned_user_id { get; set; }
    public string? reference { get; set; }
    public string? notifyowner { get; set; }
    public string? createdtime { get; set; }
    public string? modifiedtime { get; set; }
    public string? modifiedby { get; set; }
    public string? portal { get; set; }
    public string? supportstartdate { get; set; }
    public string? supportenddate { get; set; }
    public string? mailingstreet { get; set; }
    public string? otherstreet { get; set; }
    public string? mailingcity { get; set; }
    public string? othercity { get; set; }
    public string? mailingstate { get; set; }
    public string? otherstate { get; set; }
    public string? mailingzip { get; set; }
    public string? otherzip { get; set; }
    public string? mailingcountry { get; set; }
    public string? othercountry { get; set; }
    public string? mailingpobox { get; set; }
    public string? otherpobox { get; set; }
    public string? imagename { get; set; }
    public string? description { get; set; }
    public string? isconvertedfromlead { get; set; }
    public string? source { get; set; }
    public string? starred { get; set; }
    public string? tags { get; set; }
    public string? id { get; set; }
    public string? label { get; set; }
}