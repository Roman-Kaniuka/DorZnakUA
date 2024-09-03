using Domain.DorZnakUA.Dto.RoadSign;
using Domain.DorZnakUA.Result;

namespace Domain.DorZnakUA.Interfaces.Services;

/// <summary>
/// Сервіс для роботи з дорожніми знаками
/// </summary>
public interface IRoadSignService
{
    /// <summary>
    /// Отримання всіх знаків проєкта
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<CollectionResult<RoadSignDto>> GetRoadSignsAsync(long projectId);
    
    /// <summary>
    /// Отримання знака по конкретному id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<RoadSignDto>> GetRoadSignByIdAsync (long id);

    /// <summary>
    /// Створення нового знаку з базовими параметрами
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<RoadSignDto>> CreateRoadSignAsync(CreateRoadSignDto dto);

    /// <summary>
    /// Видалення знаку по вказаному id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<RoadSignDto>> DeleteRoadSignAsync(long id);

    /// <summary>
    /// Оновлення даних по знаку
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<RoadSignDto>> UpdateRoadSignAsync(UpdateRoadSignDto dto);
}