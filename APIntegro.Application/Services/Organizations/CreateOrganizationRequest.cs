using APIntegro.Application.DTOs.Organization;

namespace APIntegro.Application.Services.Organizations;

public record CreateOrganizationRequest(
        string SessionName,
        CreateOrganizationDto OrganizationDto
    );