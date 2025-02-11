namespace APIntegro.Application.Services.Organizations;

public record DeleteOrganizationRequest(
        string SessionName,
        string OrganizationId
    );
