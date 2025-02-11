using APIntegro.Application.DTOs.Contact;
using APIntegro.Application.Services.Contacts;
using APIntegro.Domain.Entities;

namespace APIntegro.Application.IHandlers;

public interface IContactHandler
{
    Task<Contact> GetContact(string sessionName, string ContactId);
    Task<IEnumerable<Contact>> GetAllContacts(string sessionName);
    Task<Contact> PostContact(string sessionName, CreateContactDto contact);
    Task<Contact> PutContact(string sessionName, UpdateContactDto contact);
    Task<bool> RemoveContact(string sessionName, string contactId);
}
