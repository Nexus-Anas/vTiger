using APIntegro.Application.DTOs.Contact;
using APIntegro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIntegro.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpPost("Create")]
    public async Task<ActionResult> CreateContact(CreateContactDto contact)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _contactService.CreateContact(contact);

        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAllContacts()
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _contactService.GetAllContacts();

        return Ok(result);
    }

    [HttpGet("Count")]
    public async Task<IActionResult> ContactsCount()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        int result = await _contactService.ContactsCount();

        return Ok(result);
    }

    [HttpGet("GetById")]
    public async Task<ActionResult> GetContact(string ContactId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _contactService.GetContact(ContactId);

        return Ok(result);
    }

    [HttpPut("Update")]
    public async Task<ActionResult> UpdateContact(UpdateContactDto contact)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _contactService.UpdateContact(contact);

        return Ok(result);
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> DeleteContact(string ContactId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _contactService.DeleteContact(ContactId);

        return Ok(result);
    }
}
