using Asp.Versioning;
using Domain.DorZnakUA.Dto.Project;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DorZnakUA.Api.Controller;
/// <summary>
/// Сервіс для роботи з проєктами
/// </summary>
/*[Authorize]*/
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
    /// Отримання проєкта по id
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Sample request to get a project by id:
    /// 
    ///     GET
    ///     {
    ///         "id": "1"
    ///     }
    /// </remarks>
    /// <response code="200">Якщо проєкт було знайдено по id</response>
    /// <response code="400">Якщо проєкт не було знайдено по id</response>
    
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

    /// <summary>
    /// Отримання проєкта користувача за userId
    /// </summary>
    /// <param name="userId"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET
    ///     {
    ///         "userId": "1"
    ///     }
    /// </remarks>
    /// <response code="200">Якщо проєкт було знайдено по userId</response>
    /// <response code="400">Якщо проєкт не було знайдено по userId</response>
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

    /// <summary>
    /// Видалення проєкту по id
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Sample request to delete a project by id:
    /// 
    ///     DELTE
    ///     {
    ///         "id": "1"
    ///     }
    /// </remarks>
    /// <response code="200">Якщо проєкт було видалено по вказаному id</response>
    /// <response code="400">Якщо проєкт не було видалено по вказаному id</response>
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

    /// <summary>
    /// Створення нового проєкта
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request to create new project:
    /// 
    ///     POST
    ///     {
    ///         "name": "Project #55",
    ///         "description": "Test project",
    ///         "userId": "1"
    ///     }
    /// </remarks>
    /// <response code="200">Якщо проєкт був створений</response>
    /// <response code="400">Якщо проєкт не був створений</response>
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

    /// <summary>
    /// Оновлення проєкту по id
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request to update a project by id:
    /// 
    ///     PUT
    ///     {
    ///         "id": "1",
    ///         "name": "Report #1",
    ///         "description": "Test report"
    ///     }
    /// </remarks>
    /// <response code="200">Якщо проєкт було оновлено по id</response>
    /// <response code="400">Якщо проєкт не було оновлено по id</response>
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