namespace APIntegro.Domain.Entities;

public record Email(
        string SendTo,
        string Subject,
        string Body
    );