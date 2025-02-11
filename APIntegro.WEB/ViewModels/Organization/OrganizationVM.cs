namespace APIntegro.WEB.ViewModels.Organization;

public record OrganizationPostVM
(
    string accountname,
    string phone,
    string fax,
    string email1,
    string? email2,
    string employees,
    string annual_revenue,
    string assigned_user_id
);


public record OrganizationPutVM
(
    string accountname,
    string phone,
    string fax,
    string email1,
    string? email2,
    string employees,
    string annual_revenue,
    string assigned_user_id,
    string id
);