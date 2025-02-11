using APIntegro.WEB.Data;
using APIntegro.Domain.Entities;
using APIntegro.WEB.Interfaces;
using APIntegro.WEB.ViewModels.Contact;

namespace APIntegro.WEB.Services;

public class ContactService : IContactService
{
    private readonly HttpClient _http;
    private readonly string? _baseUrl;
    private readonly string _controller = "Contacts";

    public ContactService(HttpClient http, IConfiguration configuration)
    {
        _http = http;
        _baseUrl = configuration.GetValue<string>("ApiSettings:APIntegroUrl");
    }



    public async Task<Contact?> FindContact(string contactId)
    {
        using var response = await _http.GetAsync($"{_baseUrl}{_controller}/GetById?contactId={contactId}").ConfigureAwait(false);
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<Contact>() : null;
    }

    public async Task<IEnumerable<Contact>?> FindAllContacts()
    {
        using var response = await _http.GetAsync($"{_baseUrl}{_controller}/GetAll");
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<IEnumerable<Contact>>() : null;
    }

    public async Task<int> ContactsCount()
    {
        using var response = await _http.GetAsync($"{_baseUrl}{_controller}/Count");
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<int>() : 0;
    }

    public async Task<(bool success, string message)> CreateContact(ContactPostVM contact)
    {
        try
        {
            using var response = await _http.PostAsJsonAsync($"{_baseUrl}{_controller}/Create", contact);

            return response.IsSuccessStatusCode
                ? (true, "Contact created with success")
                : (false, await GetErrorMessage(response, "An error occurred while creating the contact."));
        }
        catch (Exception ex)
        {
            return (false, "An error occurred: " + ex.Message);
        }
    }

    public async Task<(bool success, string message)> UpdateContact(ContactPutVM contact)
    {
        try
        {
            using var response = await _http.PutAsJsonAsync($"{_baseUrl}{_controller}/Update", contact);
            return response.IsSuccessStatusCode
                ? (true, "Contact updated successfully")
                : (false, await GetErrorMessage(response, "An error occurred while updating the contact."));
        }
        catch (Exception ex)
        {
            return (false, "An error occurred: " + ex.Message);
        }
    }

    public async Task<(bool success, string message)> DeleteContact(string contactId)
    {
        try
        {
            using var response = await _http.DeleteAsync($"{_baseUrl}{_controller}/Delete?contactId={contactId}");
            return response.IsSuccessStatusCode
                ? (true, "Contact deleted successfully")
                : (false, await GetErrorMessage(response, "An error occurred while deleting the contact."));
        }
        catch (Exception ex)
        {
            return (false, "An error occurred: " + ex.Message);
        }
    }




    private async Task<string> GetErrorMessage(HttpResponseMessage response, string errorMessage)
    {
        try
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return errorResponse?.Title ?? errorMessage;
        }
        catch { return "An error occurred while processing the response."; }
    }
}