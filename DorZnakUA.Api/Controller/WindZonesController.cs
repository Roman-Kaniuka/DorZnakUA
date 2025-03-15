using System.Net.Mime;
using Asp.Versioning;
using Domain.DorZnakUA.Dto.WindZone;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using Microsoft.AspNetCore.Mvc;

namespace DorZnakUA.Api.Controller;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class WindZonesController : ControllerBase
{
    private readonly IWindZoneService _windZoneService;

    public WindZonesController(IWindZoneService windZoneService)
    {
        _windZoneService = windZoneService;
    }

    /// <summary>
    /// Отримання всіх вітрових районів
    /// </summary>
    /// <remarks>
    /// Sample request to get a all wind zones:
    /// 
    ///     GET
    ///     {
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо вітрові райони було знайдено</response>
    /// <response code="400">Якщо вітрові райони не було знайдено</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<WindZoneDto>>> GetAllWindZones()
    {
        var response = await _windZoneService.GetAllWindZonesAsync();

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Отримання вітрового району по ідентифікатору
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Sample request to get a wind zone by id:
    /// 
    ///     GET
    ///     {
    ///         "id": "1"
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо вітровий район було знайдено по id</response>
    /// <response code="400">Якщо вітровий район не було знайдено по id</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<WindZoneDto>>> GetWindZoneById(long id)
    {
        var response = await _windZoneService.GetWindZoneByIdAsync(id);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Створення нового вітрового району
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request to create new wind zone
    ///
    ///     POST
    ///     {
    ///         "name": "Test wind zone",
    ///         "description": "Description for wind zone"
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо вітровий район було успішно створено</response>
    /// <response code="400">Якщо вітровий район було не створено</response>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<WindZoneDto>>> CreateWindZone([FromBody] CreateWindZoneDto dto)
    {
        var response = await _windZoneService.CreateWindZoneAsync(dto);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Видалення вітрового району за ідентифікатором
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Sample request to delete a wind zone by id:
    /// 
    ///     DELETE
    ///     {
    ///         "id": "1"
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо вітровий район було видалено</response>
    /// <response code="400">Якщо вітровий район не було видалено</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<WindZoneDto>>> DeleteWindZone(long id)
    {
        var response = await _windZoneService.DeleteWindZoneAsync(id);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Оновлення вітрового району
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request to update a wind zone:
    /// 
    ///     PUT
    ///     {
    ///         "id": "1",
    ///         "name": "Update test wind zone",
    ///         "description": "Description for wind zone",
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо вітровий район було оновлено</response>
    /// <response code="400">Якщо вітровий район не було оновлено</response>
    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<WindZoneDto>>> UpdateWindZone([FromBody] UpdateWindZoneDto dto)
    {
        var response = await _windZoneService.UpdateWindZoneAsync(dto);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
}