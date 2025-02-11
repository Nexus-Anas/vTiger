namespace APIntegro.Application.Services.Contacts;

public record GetContactRequest(
        string SessionName,
        string ContactId
    );
