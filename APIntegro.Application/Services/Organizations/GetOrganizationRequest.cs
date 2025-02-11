namespace APIntegro.Application.Services.Organizations;

public record GetOrganizationRequest(
        string SessionName,
        string OrganizationId
    );