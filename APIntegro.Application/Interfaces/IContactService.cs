using APIntegro.Application.DTOs.Contact;
using APIntegro.Domain.Entities;

namespace APIntegro.Application.Interfaces;

public interface IContactService
{
    Task<Contact> GetContact(string contactId);
    Task<IEnumerable<Contact>> GetAllContacts();
    Task<int> ContactsCount();
    Task<Contact> CreateContact(CreateContactDto contact);
    Task<Contact> UpdateContact(UpdateContactDto contact);
    Task<bool> DeleteContact(string contactId);
}
