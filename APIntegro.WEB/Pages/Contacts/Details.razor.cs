using APIntegro.WEB.ViewModels.Contact;
using MudBlazor;

namespace APIntegro.WEB.Pages.Contacts;

public partial class Details
{
    private async Task Update()
    {
        var request = UpdateContactRequest();
        var (success, message) = await _contactService.UpdateContact(request);

        _isLoading = false;
        BtnIcon = Icons.Material.Filled.Sync;

        Snackbar.Add(message, success ? Severity.Success : Severity.Error);
    }


    private ContactPutVM UpdateContactRequest()
    {
        BtnIcon = string.Empty;
        _isLoading = true;

        var contactPutVM = _mapper.Map<ContactPutVM>(Contact);
        return contactPutVM;
    }
}