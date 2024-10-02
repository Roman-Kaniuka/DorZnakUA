using Asp.Versioning;
using Domain.DorZnakUA.Dto.RoadSign;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using Microsoft.AspNetCore.Mvc;

namespace DorZnakUA.Api.Controller;

/// <summary>
/// Сервіс для роботи з дорожніми знаками
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class RoadSignController : ControllerBase
{
    private readonly IRoadSignService _roadSignService;

    public RoadSignController(IRoadSignService roadSignService)
    {
        _roadSignService = roadSignService;
    }

    /// <summary>
    /// Отримання всіх дорожніх знаків проєкта по projectId 
    /// </summary>
    /// <param name="projectId"></param>
    /// <remarks>
    /// Sample request to get a roadSign by projectId:
    /// 
    ///     GET
    ///     {
    ///         "projectId": "1"
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо дорожні знаки було знайдено</response>
    /// <response code="400">Якщо дорожні знаки було знайдено</response>
    [HttpGet("roadsigns/{projectId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<RoadSignDto>>> GetRoadSigns(long projectId)
    {
        var response = await _roadSignService.GetRoadSignsAsync(projectId);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Отримання дорожнього знаку по id
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Sample request to get a roadSign by id:
    /// 
    ///     GET
    ///     {
    ///         "id": "1"
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо дорожній знак було знайдено</response>
    /// <response code="400">Якщо дорожній знак не було знайдено</response>
    [HttpGet("roadsign{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<RoadSignDto>>> GetRoadSign (long id)
    {
        var response = await _roadSignService.GetRoadSignByIdAsync(id);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Створення нового дорожнього знаку
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request to create new roadSign:
    ///
    ///     POST
    ///     {
    ///         "positioning": "542+74",
    ///         "placementOnRoad": "Right",
    ///         "numberOfRacks": "2",
    ///         "projectId": "1" 
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо новий дорожній знак було створено</response>
    /// <response code="400">Якщо новий дорожній знак не було створено</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<RoadSignDto>>> CreateRoadSign([FromBody] CreateRoadSignDto dto)
    {
        var response = await _roadSignService.CreateRoadSignAsync(dto);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Видалення дорожнього знаку по id
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Sample request to delete a roadSign by id:
    /// 
    ///     DELTE
    ///     {
    ///         "id": "1"
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо дорожній знак було видалено</response>
    /// <response code="400">Якщо дорожній знак не було видалено</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<RoadSignDto>>> DeleteRoadSign(long id)
    {
        var response = await _roadSignService.DeleteRoadSignAsync(id);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Оновлення дорожнього знаку
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request to update a roadSign by id:
    ///
    ///     PUT
    ///     {
    ///         "id": "1",
    ///         "positioning": "115+98",
    ///         "placementOnRoad": "Left",
    ///         "numberOfRacks": "2"
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо дорожній знак було оновлено</response>
    /// <response code="400">Якщо дорожній знак не було оновлено</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<RoadSignDto>>> UpdateRoadSign([FromBody] UpdateRoadSignDto dto)
    {
        var response = await _roadSignService.UpdateRoadSignAsync(dto);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}