using Domain.DorZnakUA.Dto.Token;
using Domain.DorZnakUA.Dto.User;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using Microsoft.AspNetCore.Mvc;

namespace DorZnakUA.Api.Controller;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Реєстрація нового користувача
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request for registration new project:
    /// 
    ///     POST
    ///     {
    ///         "login": "Nickname",
    ///         "password": "qwerty",
    ///         "passwordConfirm": "qwerty"
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо нового користувача було зареєстровано</response>
    /// <response code="400">Якщо нового користувача не було зареєстровано</response>
    [HttpPost("register")]
    public async Task<ActionResult<BaseResult<UserDto>>> Register([FromBody]RegisterUserDto dto)
    {
        var response = await _authService.Register(dto);

        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Запит на вхід в систему
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// Sample login request:
    /// 
    ///     POST
    ///     {
    ///         "login": "Nickname",
    ///         "password": "qwerty"
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо нового користувача було зареєстровано</response>
    /// <response code="400">Якщо нового користувача не було зареєстровано</response>
    [HttpPost("login")]
    public async Task<ActionResult<BaseResult<TokenDto>>> Login([FromBody]LoginUserDto dto)
    {
        return Ok();
    }
    
    
}