using Asp.Versioning;
using Domain.DorZnakUA.Dto.Project;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DorZnakUA.Api.Controller;

//[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    /// <summary>
    /// Отримання проєкт за id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ProjectDto>>> GetProject(long id)
    {
        var response = await _projectService.GetProjectByIdAsync(id);
        if (response.IsSeccess)
        {
            return Ok(response);
        }
        
        return BadRequest(response);
    }

    [HttpGet("projects/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ProjectDto>>> GetUserProjects(long userId)
    {
        var response = await _projectService.GetProjectsAsync(userId);
        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ProjectDto>>> DeleteProject(long id)
    {
        var response = await _projectService.DeleteProject(id);
        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ProjectDto>>> CreateProject([FromBody]CreateProjectDto dto)
    {
        var response = await _projectService.CreateProjectAsync(dto);
        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ProjectDto>>> UpdateProject([FromBody]UpdateProjectDto dto)
    {
        var responce = await _projectService.UpdateProject(dto);
        if (responce.IsSeccess)
        {
            return Ok(responce);
        }

        return BadRequest(responce);
    }
}