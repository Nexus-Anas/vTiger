using APIntegro.Application.DTOs.Organization;
using APIntegro.Domain.Entities;

namespace APIntegro.Application.Interfaces;

public interface IOrganizationService
{
    Task<Organization> GetOrganization(string OrganizationId);
    Task<IEnumerable<Organization>> GetAllOrganizations();
    Task<int> OrganizationsCount();
    Task<Organization> CreateOrganization(CreateOrganizationDto organization);
    Task<Organization> UpdateOrganization(UpdateOrganizationDto organization);
    Task<bool> DeleteOrganization(string OrganizationId);
}
