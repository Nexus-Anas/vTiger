using APIntegro.Domain.Entities;
using APIntegro.MOBILE.ViewModels.Organization;

namespace APIntegro.MOBILE.Interfaces;

public interface IOrganizationService
{
    Task<Organization?> FindOrganization(string organizationId);
    Task<IEnumerable<Organization>?> FindAllOrganizations();
    Task<int> OrganizationsCount();
    Task<(bool success, string message)> CreateOrganization(OrganizationPostVM organization);
    Task<(bool success, string message)> UpdateOrganization(OrganizationPutVM organization);
    Task<(bool success, string message)> DeleteOrganization(string organizationId);
}