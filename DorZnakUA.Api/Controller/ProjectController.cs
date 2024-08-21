using Domain.DorZnakUA.Dto.Project;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DorZnakUA.Api.Controller;

//[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
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
}