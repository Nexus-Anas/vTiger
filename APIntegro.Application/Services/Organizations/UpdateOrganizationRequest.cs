using APIntegro.Application.DTOs.Organization;

namespace APIntegro.Application.Services.Organizations;

public record UpdateOrganizationRequest(
    string SessionName,
    UpdateOrganizationDto OrganizationDto
    );