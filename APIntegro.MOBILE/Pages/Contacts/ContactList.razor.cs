using APIntegro.MOBILE.Components;
using MudBlazor;

namespace APIntegro.MOBILE.Pages.Contacts;

public partial class ContactList
{
    private async Task<IEnumerable<Domain.Entities.Contact>?> LoadContacts()
        => await _contactService.FindAllContacts();


    private Func<Domain.Entities.Contact, bool> quickFilter => c =>
    {
        if (string.IsNullOrWhiteSpace(_searchQuery))
            return true;

        var searchTerms = $"{c.firstname} {c.lastname} {c.phone} {c.email} {c.birthday} {c.department}";
        return searchTerms.Contains(_searchQuery, StringComparison.OrdinalIgnoreCase);
    };


    private async Task Delete(Domain.Entities.Contact contact)
    {
        var result = await ConfirmOperation($"Are you sure you want to delete {contact.firstname} {contact.lastname}?");

        if (result.Canceled) return;


        var (success, message) = await _contactService.DeleteContact(contact.id);

        if (success)
        {
            Snackbar.Add(message, Severity.Info);
            Contacts = await LoadContacts();
            _noContactFound = !Contacts.Any();
        }
        else
            Snackbar.Add(message, Severity.Error);
    }


    private async Task<DialogResult> ConfirmOperation(string dialogueMessage)
    {
        var options = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, ClassBackground = "my-custom-class" };
        var @params = new DialogParameters<RemoveDialog> { { x => x.Message, dialogueMessage } };
        var dialog = await DialogService.ShowAsync<RemoveDialog>("Remove Contact", @params, options);
        return await dialog.Result;
    }



    private MudBlazor.Color GetDepartmentColor(string value)
    {
        return value switch
        {
            string d when d == "Human Resources" => MudBlazor.Color.Primary,
            string d when d == "Finance" => MudBlazor.Color.Secondary,
            string d when d == "Marketing" => MudBlazor.Color.Success,
            string d when d == "Sales" => MudBlazor.Color.Tertiary,
            string d when d == "IT" => MudBlazor.Color.Error,
            string d when d == "Operations" => MudBlazor.Color.Info,
            string d when d == "Customer Support" => MudBlazor.Color.Warning,
            _ => MudBlazor.Color.Dark
        };
    }
}