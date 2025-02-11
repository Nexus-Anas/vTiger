using System.ComponentModel.DataAnnotations;

namespace APIntegro.Domain.Entities;

public class Organization
{
    [Required(ErrorMessage = "Organization name is required.")]
    public string accountname { get; set; }
    public string? account_no { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    public string phone { get; set; }
    public string? website { get; set; }

    [Required(ErrorMessage = "Fax is required.")]
    public string fax { get; set; }
    public string? tickersymbol { get; set; }
    public string? otherphone { get; set; }
    public string? account_id { get; set; }

    [EmailAddress(ErrorMessage = "Please enter a valid email address."), Required(ErrorMessage = "Email address is required.")]
    public string email1 { get; set; }

    [Required(ErrorMessage = "Number of employees is required.")]
    public string employees { get; set; }
    public string? email2 { get; set; }
    public string? ownership { get; set; }
    public string? rating { get; set; }
    public string? industry { get; set; }
    public string? siccode { get; set; }
    public string? accounttype { get; set; }

    [Required(ErrorMessage = "Annual revenue is required.")]
    public string annual_revenue { get; set; }
    public string? emailoptout { get; set; }
    public string? notify_owner { get; set; }
    public string? assigned_user_id { get; set; }
    public string? createdtime { get; set; }
    public string? modifiedtime { get; set; }
    public string? modifiedby { get; set; }
    public string? bill_street { get; set; }
    public string? ship_street { get; set; }
    public string? bill_city { get; set; }
    public string? ship_city { get; set; }
    public string? bill_state { get; set; }
    public string? ship_state { get; set; }
    public string? bill_code { get; set; }
    public string? ship_code { get; set; }
    public string? bill_country { get; set; }
    public string? ship_country { get; set; }
    public string? bill_pobox { get; set; }
    public string? ship_pobox { get; set; }
    public string? description { get; set; }
    public string? isconvertedfromlead { get; set; }
    public string? source { get; set; }
    public string? starred { get; set; }
    public string? tags { get; set; }
    public string? id { get; set; }
}
