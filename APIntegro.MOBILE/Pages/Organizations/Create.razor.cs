using APIntegro.Domain.Entities;
using APIntegro.MOBILE.ViewModels.Organization;
using MudBlazor;

namespace APIntegro.MOBILE.Pages.Organizations;

public partial class Create
{
    private async Task Submit()
    {
        var request = CreateOrganizationRequest();
        var (success, message) = await _organizationService.CreateOrganization(request);

        _isLoading = false;
        BtnIcon = Icons.Material.Filled.FileDownloadDone;

        Snackbar.Add(message, success ? Severity.Info : Severity.Error);

        if (success) ClearFields();
    }


    private OrganizationPostVM CreateOrganizationRequest()
    {
        BtnIcon = string.Empty;
        _isLoading = true;

        Organization.assigned_user_id = Session.User.userId;
        var organizationPostVM = _mapper.Map<OrganizationPostVM>(Organization);
        return organizationPostVM;
    }


    private void ClearFields()
    {
        AnnualRevenue = 0.00m;
        Organization = new();
    }
}