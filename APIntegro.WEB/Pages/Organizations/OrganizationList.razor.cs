using APIntegro.WEB.Components;
using APIntegro.Domain.Entities;
using MudBlazor;

namespace APIntegro.WEB.Pages.Organizations;

public partial class OrganizationList
{
    private async Task<IEnumerable<Organization>?> LoadOrganizations()
        => await _organizationService.FindAllOrganizations();


    private Func<Organization, bool> quickFilter => o =>
    {
        if (string.IsNullOrWhiteSpace(_searchQuery))
            return true;

        var searchTerms = $"{o.accountname} {o.phone} {o.fax} {o.email1} {o.email2} {o.employees} {o.annual_revenue}";
        return searchTerms.Contains(_searchQuery, StringComparison.OrdinalIgnoreCase);
    };


    private async Task Delete(Organization organization)
    {
        var result = await ConfirmOperation($"Are you sure you want to delete {organization.accountname}?");

        if (result.Canceled) return;


        var (success, message) = await _organizationService.DeleteOrganization(organization.id);

        if (success)
        {
            Snackbar.Add(message, Severity.Info);
            Organizations = await LoadOrganizations();
            _noOrganizationFound = !Organizations.Any();
        }
        else
            Snackbar.Add(message, Severity.Error);
    }


    private async Task<DialogResult> ConfirmOperation(string dialogueMessage)
    {
        var options = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, ClassBackground = "my-custom-class" };
        var @params = new DialogParameters<RemoveDialog> { { x => x.Message, dialogueMessage } };
        var dialog = await DialogService.ShowAsync<RemoveDialog>("Remove Organization", @params, options);
        return await dialog.Result;
    }
}