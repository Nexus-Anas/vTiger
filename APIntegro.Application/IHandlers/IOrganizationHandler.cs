using APIntegro.Application.DTOs.Organization;
using APIntegro.Application.Services.Contacts;
using APIntegro.Application.Services.Organizations;
using APIntegro.Domain.Entities;

namespace APIntegro.Application.IHandlers;

public interface IOrganizationHandler
{
    Task<Organization> GetOrganization(string sessionName, string organizationId);
    Task<IEnumerable<Organization>> GetAllOrganizations(string sessionName);
    Task<Organization> PostOrganization(string sessionName, CreateOrganizationDto organization);
    Task<Organization> PutOrganization(string sessionName, UpdateOrganizationDto organization);
    Task<bool> RemoveOrganization(string sessionName, string organizationId);
}
