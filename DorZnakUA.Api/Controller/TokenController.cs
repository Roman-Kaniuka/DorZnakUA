using Domain.DorZnakUA.Dto.Token;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using Microsoft.AspNetCore.Mvc;

namespace DorZnakUA.Api.Controller;

/// <summary>
/// Сервіс для роботи з токінами
/// </summary>
[ApiController]
[Route("refresh")]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    /// <summary>
    /// Запит на отримання RefreshToken
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// A sample request for a RefreshToken:
    /// 
    ///     POST
    ///     {
    ///         "accessToken": "AccessToken",
    ///         "refreshToken": "RefreshToken",
    ///     }
    /// </remarks>>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<TokenDto>>> RefreshToken ([FromBody]TokenDto dto)
    {
        var response = await _tokenService.RefreshTokenAsync(dto);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
}