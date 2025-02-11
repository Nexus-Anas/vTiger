using APIntegro.Application.DTOs.Organization;
using APIntegro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIntegro.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationsController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpPost("Create")]
    public async Task<ActionResult> CreateContact(CreateOrganizationDto organization)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _organizationService.CreateOrganization(organization);

        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAllContacts()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _organizationService.GetAllOrganizations();

        return Ok(result);
    }

    [HttpGet("Count")]
    public async Task<IActionResult> OrganizationsCount()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        int result = await _organizationService.OrganizationsCount();

        return Ok(result);
    }

    [HttpGet("GetById")]
    public async Task<ActionResult> GetContact(string organizationId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _organizationService.GetOrganization(organizationId);

        return Ok(result);
    }

    [HttpPut("Update")]
    public async Task<ActionResult> UpdateContact(UpdateOrganizationDto organization)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _organizationService.UpdateOrganization(organization);

        return Ok(result);
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> DeleteContact(string OrganizationId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _organizationService.DeleteOrganization(OrganizationId);

        return Ok(result);
    }

}
