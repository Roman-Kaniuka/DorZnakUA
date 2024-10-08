using Domain.DorZnakUA.Dto.WindZone;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Result;

namespace Domain.DorZnakUA.Interfaces.Services;

/// <summary>
/// Сервіс відповідає за роботу з вітровим районом
/// </summary>
public interface IWindZoneService
{
    /// <summary>
    /// Отримання всіх вітрових районів
    /// </summary>
    /// <returns></returns>
    Task<CollectionResult<WindZoneDto>> GetAllWindZonesAsync();

    /// <summary>
    ///  Отримання одного вітрового району по ідентифікатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<WindZoneDto>> GetWindZoneByIdAsync(long id);

    /// <summary>
    /// Створення нового вітрового району
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<WindZoneDto>> CreateWindZoneAsync(CreateWindZoneDto dto);

    /// <summary>
    /// Видалення вітрового району по вказаному id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<WindZoneDto>> DeleteWindZoneAsync(long id);

    /// <summary>
    /// Оновлення даних про вітровий район
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<WindZoneDto>> UpdateWindZoneAsync(UpdateWindZoneDto dto);
}