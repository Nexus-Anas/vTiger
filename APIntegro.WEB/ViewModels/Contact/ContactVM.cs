namespace APIntegro.WEB.ViewModels.Contact;

public record ContactPostVM
(
    string firstname,
    string lastname,
    string phone,
    string email,
    string birthday,
    string? department,
    string assigned_user_id
);


public record ContactPutVM
(
    string firstname,
    string lastname,
    string phone,
    string email,
    string birthday,
    string? department,
    string assigned_user_id,
    string id
);