using APIntegro.Application.Interfaces;
using APIntegro.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIntegro.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    public ProjectController(IProjectService projectService) => _projectService = projectService;




    [HttpGet("Find")]
    public async Task<IActionResult> FindProject(string projectId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _projectService.FindProject(projectId);

        return Ok(result);
    }

    [HttpGet("FindAll")]
    public async Task<IActionResult> FindAllProjects()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _projectService.FindAllProjects();

        return Ok(result);
    }

    [HttpGet("Count")]
    public async Task<IActionResult> ProjectsCount()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        int result = await _projectService.ProjectsCount();

        return Ok(result);
    }

    [HttpGet("StatesChart")]
    public async Task<IActionResult> ProjectsStatesChart()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _projectService.ProjectsStatesChart();

        return Ok(result);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateProject(Project project)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _projectService.CreateProject(project);

        return Ok(result);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateProject(Project project)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _projectService.UpdateProject(project);

        return Ok(result);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteProject(string projectId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _projectService.DeleteProject(projectId);

        return Ok(result);
    }
}