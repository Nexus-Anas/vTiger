using APIntegro.Application.DTOs.Contact;
using APIntegro.Application.IHandlers;
using APIntegro.Application.Interfaces;
using APIntegro.Domain.Entities;
using APIntegro.Application.Services.Authentication;

namespace APIntegro.Application.Services.Contacts;

public class ContactService : IContactService
{
    private readonly IContactHandler _contactHandler;
    private readonly LoginSession _session;

    public ContactService(IContactHandler contactHandler, LoginSession session)
    {
        _contactHandler = contactHandler;
        _session = session;
    }

    public async Task<Contact> CreateContact(CreateContactDto contact)
    {
        return await _contactHandler.PostContact(_session.User.sessionName, contact);
    }

    public async Task<IEnumerable<Contact>> GetAllContacts()
    {
        return await _contactHandler.GetAllContacts(_session.User.sessionName);
        

    }

    public async Task<int> ContactsCount()
    {
        var result = await _contactHandler.GetAllContacts(_session.User.sessionName);
        return result.Count();
    }

    public async Task<Contact> GetContact(string contactId)
    {
        return await _contactHandler.GetContact(_session.User.sessionName, contactId);
    }

    public async Task<Contact> UpdateContact(UpdateContactDto contact)
    {
        return await _contactHandler.PutContact(_session.User.sessionName, contact);
    }
    public async Task<bool> DeleteContact(string contactId)
    {
        return await _contactHandler.RemoveContact(_session.User.sessionName, contactId);
    }

}
