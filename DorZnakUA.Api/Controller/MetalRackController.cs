using Asp.Versioning;
using Domain.DorZnakUA.Dto.MetalRack;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DorZnakUA.Api.Controller;

/// <summary>
/// Сервіс для роботи зі стійками
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MetalRackController : ControllerBase
{
    private readonly IMetalRackService _metalRackService;

    public MetalRackController(IMetalRackService metalRackService)
    {
        _metalRackService = metalRackService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "Admin, Moderator")]
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
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="roadSignId"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
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
}
