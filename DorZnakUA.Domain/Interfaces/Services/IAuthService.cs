using Domain.DorZnakUA.Dto.Token;
using Domain.DorZnakUA.Dto.User;
using Domain.DorZnakUA.Result;

namespace Domain.DorZnakUA.Interfaces.Services;

/// <summary>
/// Сервіс призначений для реєстрації/авторизації
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Реєстрація нового користувача
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<UserDto>> Register(RegisterUserDto dto);

    /// <summary>
    /// Авторизація користувача
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<TokenDto>> Login(LoginUserDto dto);
}