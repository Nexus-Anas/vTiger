using APIntegro.Application.Common.Errors;
using APIntegro.Application.DTOs.Contact;
using APIntegro.Application.IHandlers;
using APIntegro.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APIntegro.Infrastructure.Handlers;

public class ContactHandler : IContactHandler
{
    private readonly IApiHandler _apiHandler;

    public ContactHandler(IApiHandler apiHandler)
    {
        _apiHandler = apiHandler;
    }

    public async Task<IEnumerable<Contact>> GetAllContacts(string sessionName)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "query" },
            { "sessionName", sessionName },
            { "query", "SELECT * FROM Contacts;" }
        };

        var response = await _apiHandler.GetApiResponse(urlParams);

        ThrowIfError(response);

        return response["result"].ToObject<IEnumerable<Contact>>();
    }

    public async Task<Contact> GetContact(string sessionName, string ContactId)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "retrieve" },
            { "sessionName", sessionName },
            { "id", ContactId }
        };

        var response = await _apiHandler.GetApiResponse(urlParams);

        ThrowIfError(response);

        return response["result"].ToObject<Contact>();
    }

    public async Task<Contact> PostContact(string sessionName, CreateContactDto contact)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "create" },
            { "sessionName", sessionName },
            { "element", JsonConvert.SerializeObject(contact) },
            { "elementType", "Contacts" }
        };

        var response = await _apiHandler.PostApiResponse(urlParams);

        ThrowIfError(response);

        return response["result"].ToObject<Contact>();

    }

    public async Task<Contact> PutContact(string sessionName, UpdateContactDto contact)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "update" },
            { "sessionName", sessionName },
            { "element", JsonConvert.SerializeObject(contact) },
            { "elementType", "Contacts" }
        };

        var response = await _apiHandler.PostApiResponse(urlParams);

        ThrowIfError(response);

        return response["result"].ToObject<Contact>();
    }

    public async Task<bool> RemoveContact(string sessionName, string contactId)
    {
        var urlParams = new Dictionary<string, string>
        {
            { "operation", "delete" },
            { "sessionName", sessionName },
            { "id", contactId }
        };

        var response = await _apiHandler.PostApiResponse(urlParams);
        Console.WriteLine(response);

        ThrowIfError(response);

        return response["success"].Value<bool>();
    }

    static void ThrowIfError(JObject response)
    {
        if (response is null || !response["success"].Value<bool>())
        {
            var errorObject = response["error"];
            var errorMessage = errorObject["message"].Value<string>();
            var errorCode = errorObject["code"].Value<string>();

            throw new VtigerException(errorCode, errorMessage);
        }
    }


}
