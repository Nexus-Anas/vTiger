using APIntegro.Application.DTOs.Contact;

namespace APIntegro.Application.Services.Contacts;

public record UpdateContactRequest(
        string SessionName,
        UpdateContactDto Contact
    );

