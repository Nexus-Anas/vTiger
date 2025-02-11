using APIntegro.Application.DTOs.ProjectMilestone;
using APIntegro.Application.Interfaces;
using APIntegro.Application.Services.ProjectMilestones;
using APIntegro.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIntegro.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectMilestonesController : ControllerBase
{
    private readonly IProjectMilestoneService _projectMilestoneService;

    public ProjectMilestonesController(IProjectMilestoneService projectMilestoneService)
    {
        _projectMilestoneService = projectMilestoneService;
    }

    [HttpPost("Create")]
    public async Task<ActionResult> CreateProjectMilestone(CreateProjectMilestoneDto projectMilestone)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _projectMilestoneService.CreateProjectMilestone(projectMilestone);

        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAllProjectMilestones()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _projectMilestoneService.GetAllProjectMilestones();

        return Ok(result);
    }

    [HttpGet("GetById")]
    public async Task<ActionResult> GetProjectMilestone(string projectMilestoneId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _projectMilestoneService.GetProjectMilestone(projectMilestoneId);

        return Ok(result);
    }

    [HttpPut("Update")]
    public async Task<ActionResult> UpdateProjectMilestone(UpdateProjectMilestoneDto projectMilestone)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _projectMilestoneService.UpdateProjectMilestone(projectMilestone);

        return Ok(result);
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> DeleteContact(string projectMilestoneId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _projectMilestoneService.DeleteProjectMilestone(projectMilestoneId);

        return Ok(result);
    }
}
