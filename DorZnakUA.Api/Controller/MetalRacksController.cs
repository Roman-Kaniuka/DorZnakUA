using System.Net.Mime;
using Asp.Versioning;
using Domain.DorZnakUA.Dto.MetalRack;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using DorZnakUA.Application.Validations.FluentValidations.MetalRack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DorZnakUA.Api.Controller;

/// <summary>
/// Сервіс для роботи зі стійками
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MetalRacksController : ControllerBase
{
    private readonly IMetalRackService _metalRackService;

    public MetalRacksController(IMetalRackService metalRackService)
    {
        _metalRackService = metalRackService;
    }

    /// <summary>
    /// Отримання всіх стійок
    /// </summary>
    /// <remarks>
    /// Sample request to get a all metal racks:
    /// 
    ///     GET
    ///     {
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо металеву стійки було знайдено</response>
    /// <response code="400">Якщо металеву стійки не було знайдено</response>
    [HttpGet]
    /*[Authorize(Roles = "Admin, Moderator")]*/
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<MetalRackDto>>> GetAllMetalRacks()
    {
        var response = await _metalRackService.GetAllMetalRacksAsync();

        if (response.IsSeccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    /// <summary>
    /// Отрмання стійки по ідентифікатору
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Sample request to get a metal rack by id:
    /// 
    ///     GET
    ///     {
    ///         "id": "1"
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо металеву стійку було знайдено по id</response>
    /// <response code="400">Якщо металеву стійку не було знайдено по id</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<MetalRackDto>>> GetMetalRack(long id)
    {
        var response = await _metalRackService.GetMetalRackByIdAsync(id);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Тримання стійки знаку по його ідентифікатору
    /// </summary>
    /// <param name="roadSignId"></param>
    /// <remarks>
    /// Sample request to get a road sign metal rack by roadSignId:
    /// 
    ///     GET
    ///     {
    ///         "roadSignId": "1"
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо металеву стійку знаку було знайдено по roadSignId</response>
    /// <response code="400">Якщо металеву стійку знаку не було знайдено по roadSignId</response>
    [HttpGet("rack/{roadSignId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<MetalRackDto>>> GetRoadSignMetalRack(long roadSignId)
    {
        var response = await _metalRackService.GetRoadSignMetalRackAsync(roadSignId);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Створення нової стійки
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request to create new metal rack
    ///
    ///     POST
    ///     {
    ///         "name": "СКМ1.35",
    ///         "height": "3.5",
    ///         "weight": "9.6"
    ///         "diameter": "0.04"
    ///         "thickness": "0.003"
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо металеву стійку було успішно створено</response>
    /// <response code="400">Якщо металеву стійку було не створено</response>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<MetalRackDto>>> CreateMetalRack([FromBody] CreateMetalRackDto dto)
    {
        var response = await _metalRackService.CreateMetalRackAsync(dto);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Видалення стійки за ідентифікатором
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Sample request to delete a metal rack by id:
    /// 
    ///     DELTE
    ///     {
    ///         "id": "1"
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо металеву стійку було видалено</response>
    /// <response code="400">Якщо металеву стійку не було видалено</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<MetalRackDto>>> DeleteMetalRack(long id)
    {
        var response = await _metalRackService.DeleteMetalRackAsync(id);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Оновлення стійки
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request to update a metal rack:
    /// 
    ///     PUT
    ///     {
    ///         "id": "1",
    ///         "name": "СКМ1.35",
    ///         "height": "3.5",
    ///         "weight": "9.6",
    ///         "diameter": "0.04",
    ///         "thickness": "0.003"
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо металеву стійку було оновлено</response>
    /// <response code="400">Якщо металеву стійку не було оновлено</response>
    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<MetalRackDto>>> UpdateMetalRack([FromBody] UpdateMetalRackDto dto)
    {
        var response = await _metalRackService.UpdateMetalRackAsync(dto);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Підрахунок висоти стійки для дорожнього знаку
    /// </summary>
    /// <param name="roadSignId"></param>
    /// <remarks>
    /// Sample request to calculate a metal rack height:
    /// 
    ///     POST
    ///     {
    ///         "roadSignId": "3",   
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо металеву стійку було оновлено</response>
    /// <response code="400">Якщо металеву стійку не було оновлено</response>
    [HttpPost("calculate-metal-rack/{roadSignId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<MetalRackDto>>> CalculateRackHeight(long roadSignId)
    {
        var response = await _metalRackService.CalculateRackHeightAsync(roadSignId);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);

    }
    
}
