using Asp.Versioning;
using Domain.DorZnakUA.Dto.RoadSign;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using Microsoft.AspNetCore.Mvc;

namespace DorZnakUA.Api.Controller;

/// <summary>
/// 
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
    /// 
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpGet("roadsigns{projectId}")]
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
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("roadsign{id}")]
    public async Task<ActionResult<BaseResult<RoadSignDto>>> GetRoadSign (long id)
    {
        var response = await _roadSignService.GetRoadSignByIdAsync(id);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}