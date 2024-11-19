using Domain.DorZnakUA.Dto.Shield;
using Domain.DorZnakUA.Result;

namespace Domain.DorZnakUA.Interfaces.Services;

/// <summary>
/// Сервіс відповідає за роботу з щитками дорожніх знаків
/// </summary>
public interface IShieldService
{
    /// <summary>
    /// Отримання всіх щитів
    /// </summary>
    /// <returns></returns>
    Task<CollectionResult<ShieldDto>> GetAllShieldsAsync();

    /// <summary>
    /// Отримання одного щита по ідентифікатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ShieldDto>> GetShieldByIdAsync(long id);

    /// <summary>
    /// Отримання щитів дорожнього знака за його ідентифікатором
    /// </summary>
    /// <param name="roadSignId"></param>
    /// <returns></returns>
    Task<CollectionResult<ShieldDto>> GetRoadSignShieldsAsync(long roadSignId);

    /// <summary>
    /// Створення нового щита
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<ShieldDto>> CreateShieldAsync(CreateShieldDto dto);

    /// <summary>
    /// Видалення наявного щитка по ідентифікатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ShieldDto>> DeleteShieldAsync(long id);

    /// <summary>
    /// Оновлення інформації про дорожній щит
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<ShieldDto>> UpdateShieldAsync(UpdateShieldDto dto);


}