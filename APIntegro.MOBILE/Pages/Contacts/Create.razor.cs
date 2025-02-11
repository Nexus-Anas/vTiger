using APIntegro.Domain.Entities;
using APIntegro.MOBILE.ViewModels.Contact;
using MudBlazor;

namespace APIntegro.MOBILE.Pages.Contacts;

public partial class Create
{
    private async Task Submit()
    {
        var request = CreateContactRequest();
        var (success, message) = await _contactService.CreateContact(request);

        _isLoading = false;
        BtnIcon = Icons.Material.Filled.FileDownloadDone;

        Snackbar.Add(message, success ? Severity.Info : Severity.Error);

        if (success) ClearFields();
    }


    private ContactPostVM CreateContactRequest()
    {
        BtnIcon = string.Empty;
        _isLoading = true;

        Contact.assigned_user_id = Session.User.userId;
        var contactPostVM = _mapper.Map<ContactPostVM>(Contact);
        return contactPostVM;
    }


    private void ClearFields()
    {
        Birthday = null;
        Contact = new();
    }
}