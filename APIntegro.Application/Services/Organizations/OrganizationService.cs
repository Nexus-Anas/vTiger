using APIntegro.Application.DTOs.Organization;
using APIntegro.Application.IHandlers;
using APIntegro.Application.Interfaces;
using APIntegro.Domain.Entities;
using APIntegro.Application.Services.Authentication;

namespace APIntegro.Application.Services.Organizations;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationHandler _organizationHandler;
    private readonly LoginSession _session;

    public OrganizationService(IOrganizationHandler organizationHandler, LoginSession session)
    {
        _organizationHandler = organizationHandler;
        _session = session;
    }

    public async Task<Organization> CreateOrganization(CreateOrganizationDto organization)
    {
        return await _organizationHandler.PostOrganization(_session.User.sessionName, organization);
    }

    public async Task<bool> DeleteOrganization(string OrganizationId)
    {
        return await _organizationHandler.RemoveOrganization(_session.User.sessionName, OrganizationId);
    }

    public async Task<int> OrganizationsCount()
    {
        var result = await _organizationHandler.GetAllOrganizations(_session.User.sessionName);
        return result.Count();
    }

    public async Task<IEnumerable<Organization>> GetAllOrganizations()
    {
        return await _organizationHandler.GetAllOrganizations(_session.User.sessionName);
    }

    public async Task<Organization> GetOrganization(string organizationId)
    {
        return await _organizationHandler.GetOrganization(_session.User.sessionName, organizationId);
    }

    public async Task<Organization> UpdateOrganization(UpdateOrganizationDto organization)
    {
        return await _organizationHandler.PutOrganization(_session.User.sessionName, organization);
    }
}
