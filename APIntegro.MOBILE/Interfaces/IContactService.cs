using APIntegro.MOBILE.ViewModels.Contact;

namespace APIntegro.MOBILE.Interfaces;

public interface IContactService
{
    Task<Domain.Entities.Contact?> FindContact(string contactId);
    Task<IEnumerable<Domain.Entities.Contact>?> FindAllContacts();
    Task<int> ContactsCount();
    Task<(bool success, string message)> CreateContact(ContactPostVM contact);
    Task<(bool success, string message)> UpdateContact(ContactPutVM contact);
    Task<(bool success, string message)> DeleteContact(string contactId);
}