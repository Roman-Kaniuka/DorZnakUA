using System.Net.Mime;
using Asp.Versioning;
using Domain.DorZnakUA.Dto.Shield;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using Microsoft.AspNetCore.Mvc;

namespace DorZnakUA.Api.Controller;

/// <summary>
/// Сервіс для роботи з щитками дорожніх знаків
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ShieldsController : ControllerBase
{
    private readonly IShieldService _shieldService;

    public ShieldsController(IShieldService shieldService)
    {
        _shieldService = shieldService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Sample request to get a all shields:
    /// 
    ///     GET
    ///     {
    ///     }
    /// </remarks>
    /// <response code="200">Якщо щиткі було успішно отримано</response>
    /// <response code="400">Якщо щиткі не було успішно отримано</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CollectionResult<ShieldDto>>> GetAllShields()
    {
        var responce = await _shieldService.GetAllShieldsAsync();

        if (responce.IsSeccess)
        {
            return Ok(responce);
        }

        return BadRequest(responce);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Sample request to get a shield by id:
    /// 
    ///     GET
    ///     {
    ///         "id": "1"
    ///     }
    /// </remarks>
    /// <response code="200">Якщо щит за вказаним id було знайдено</response>
    /// <response code="400">Якщо щит за вказаним id не було знайдено</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResult<ShieldDto>>> GetShieldById(long id)
    {
        var responce = await _shieldService.GetShieldByIdAsync(id);

        if (responce.IsSeccess)
        {
            return Ok(responce);
        }

        return BadRequest(responce);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="roadSignId"></param>
    /// <remarks>
    /// Sample request to get a road sign shields by roadSignId:
    /// 
    ///     GET
    ///     {
    ///         "roadSignId": "1"
    ///     }
    /// </remarks>
    /// <response code="200">Якщо щиткі знаку за вказаним roadSignId було знайдено</response>
    /// <response code="400">Якщо щиткі знаку за вказаним roadSignId не було знайдено</response>
    [HttpGet("shields/{roadSignId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CollectionResult<ShieldDto>>> GetRoadSignShields(long roadSignId)
    {
        var responce = await _shieldService.GetRoadSignShieldsAsync(roadSignId);

        if (responce.IsSeccess)
        {
            return Ok(responce);
        }

        return BadRequest(responce);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request to create new shield
    ///
    ///     POST
    ///     {
    ///         "group": "ProhibitionSigns",
    ///         "name": "3.21",
    ///         "shape": "Circle",
    ///         "sizeType": "I",
    ///         "height": "0.6",
    ///         "width": "0.6",
    ///         "weight": "6.2"
    ///     }
    /// </remarks>
    /// <response code="200">Якщо щит було успішно створено</response>
    /// <response code="400">Якщо щит не було успішно створено</response>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ShieldDto>>> CreateShield([FromBody] CreateShieldDto dto)
    {
        var responce = await _shieldService.CreateShieldAsync(dto);

        if (responce.IsSeccess)
        {
            return Ok(responce);
        }

        return BadRequest(responce);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Sample request to delete shield
    ///
    ///     POST
    ///     {
    ///         "id": "2"
    ///     }
    /// </remarks>
    /// <response code="200">Якщо щит було успішно видалено</response>
    /// <response code="400">Якщо щит не було успішно видалено</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ShieldDto>>> DeleteShield(long id)
    {
        var responce = await _shieldService.DeleteShieldAsync(id);

        if (responce.IsSeccess)
        {
            return Ok(responce);
        }

        return BadRequest(responce);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request to update shield
    ///
    ///     POST
    ///     {
    ///         "id": "2",
    ///         "group": "ProhibitionSigns",
    ///         "name": "3.21",
    ///         "shape": "Circle",
    ///         "sizeType": "I",
    ///         "height": "0.6",
    ///         "width": "0.6",
    ///         "weight": "6.2"
    ///     }
    /// </remarks>
    /// <response code="200">Якщо щит було успішно оновлено</response>
    /// <response code="400">Якщо щит не було успішно оновлено</response>
    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ShieldDto>>> UpdateShield([FromBody] UpdateShieldDto dto)
    {
        var responce = await _shieldService.UpdateShieldAsync(dto);

        if (responce.IsSeccess)
        {
            return Ok(responce);
        }

        return BadRequest(responce);
    }
    
}