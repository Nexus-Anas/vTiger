using APIntegro.WEB.Components;
using APIntegro.Domain.Entities;
using MudBlazor;

namespace APIntegro.WEB.Pages.Contacts;

public partial class ContactList
{
    private async Task<IEnumerable<Contact>?> LoadContacts()
        => await _contactService.FindAllContacts();


    private Func<Contact, bool> quickFilter => c =>
    {
        if (string.IsNullOrWhiteSpace(_searchQuery))
            return true;

        var searchTerms = $"{c.firstname} {c.lastname} {c.phone} {c.email} {c.birthday} {c.department}";
        return searchTerms.Contains(_searchQuery, StringComparison.OrdinalIgnoreCase);
    };


    private async Task Delete(Contact contact)
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



    private Color GetDepartmentColor(string value)
    {
        return value switch
        {
            string d when d == "Human Resources" => Color.Primary,
            string d when d == "Finance" => Color.Secondary,
            string d when d == "Marketing" => Color.Success,
            string d when d == "Sales" => Color.Tertiary,
            string d when d == "IT" => Color.Error,
            string d when d == "Operations" => Color.Info,
            string d when d == "Customer Support" => Color.Warning,
            _ => Color.Dark
        };
    }
}