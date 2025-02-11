namespace APIntegro.Application.Services.Contacts;

public record DeleteContactRequest(
        string SessionName,
        string ContactId
    );
