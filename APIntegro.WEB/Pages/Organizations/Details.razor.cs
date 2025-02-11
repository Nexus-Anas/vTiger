using APIntegro.WEB.ViewModels.Organization;
using MudBlazor;

namespace APIntegro.WEB.Pages.Organizations;

public partial class Details
{
    private async Task Update()
    {
        var request = UpdateOrganizationRequest();
        var (success, message) = await _organizationService.UpdateOrganization(request);

        _isLoading = false;
        BtnIcon = Icons.Material.Filled.Sync;

        Snackbar.Add(message, success ? Severity.Success : Severity.Error);
    }


    private OrganizationPutVM UpdateOrganizationRequest()
    {
        BtnIcon = string.Empty;
        _isLoading = true;

        var organizationPutVM = _mapper.Map<OrganizationPutVM>(Organization);
        return organizationPutVM;
    }
}