using APIntegro.Domain.Entities;
using APIntegro.WEB.ViewModels.Contact;

namespace APIntegro.WEB.Interfaces;

public interface IContactService
{
    Task<Contact?> FindContact(string contactId);
    Task<IEnumerable<Contact>?> FindAllContacts();
    Task<int> ContactsCount();
    Task<(bool success, string message)> CreateContact(ContactPostVM contact);
    Task<(bool success, string message)> UpdateContact(ContactPutVM contact);
    Task<(bool success, string message)> DeleteContact(string contactId);
}