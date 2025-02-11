using APIntegro.Application.DTOs.Contact;

namespace APIntegro.Application.Services.Contacts;

public record CreateContactRequest(
        string SessionName,
        CreateContactDto Contact
    );


