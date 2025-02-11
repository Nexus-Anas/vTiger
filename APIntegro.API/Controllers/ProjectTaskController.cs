using APIntegro.Application.Interfaces;
using APIntegro.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIntegro.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectTaskController : ControllerBase
{
    private readonly IProjectTaskService _projectTaskService;
    public ProjectTaskController(IProjectTaskService projectTaskService) => _projectTaskService = projectTaskService;




    [HttpGet("Find")]
    public async Task<IActionResult> FindProjectTask(string projectTaskId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _projectTaskService.FindProjectTask(projectTaskId);

        return Ok(result);
    }

    [HttpGet("FindAll")]
    public async Task<IActionResult> FindAllProjectTasks()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _projectTaskService.FindAllProjectTasks();

        return Ok(result);
    }

    [HttpGet("Count")]
    public async Task<IActionResult> ProjectTasksCount()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        int result = await _projectTaskService.ProjectTasksCount();

        return Ok(result);
    }

    [HttpGet("StatesChart")]
    public async Task<IActionResult> ProjectsStatesChart()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _projectTaskService.ProjectTasksStatesChart();

        return Ok(result);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateProject(ProjectTask projectTask)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _projectTaskService.CreateProjectTask(projectTask);

        return Ok(result);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateProject(ProjectTask projectTask)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _projectTaskService.UpdateProjectTask(projectTask);

        return Ok(result);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteProject(string projectTaskId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _projectTaskService.DeleteProjectTask(projectTaskId);

        return Ok(result);
    }
}