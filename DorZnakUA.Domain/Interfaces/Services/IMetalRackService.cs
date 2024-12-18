using Domain.DorZnakUA.Dto.MetalRack;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Result;

namespace Domain.DorZnakUA.Interfaces.Services;

public interface IMetalRackService
{
    /// <summary>
    /// Отримання всіх існуючих стійок
    /// </summary>
    /// <returns></returns>
    Task<CollectionResult<MetalRackDto>> GetAllMetalRacksAsync();
    
    /// <summary>
    /// Отримання стійки за її ідентифікатором
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<MetalRackDto>> GetMetalRackByIdAsync(long id);
    
    /// <summary>
    /// Отримання стійки знаку за його ідентифікатором
    /// </summary>
    /// <param name="roadSignId"></param>
    /// <returns></returns>
    Task<BaseResult<MetalRackDto>> GetRoadSignMetalRackAsync(long roadSignId);
    
    /// <summary>
    /// Створення нової стійки
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<MetalRackDto>> CreateMetalRackAsync(CreateMetalRackDto dto);

    /// <summary>
    /// Видалення стійки по ідентифікатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<MetalRackDto>> DeleteMetalRackAsync (long id);

    /// <summary>
    /// Оновлення даних про стійку
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<MetalRackDto>> UpdateMetalRackAsync(UpdateMetalRackDto dto);

    /// <summary>
    /// Обчислює висота та кількісь стійок для знаку
    /// </summary>
    /// <param name="roadSignId"></param>
    /// <returns></returns>
    Task<BaseResult<MetalRackDto>> CalculateRackHeightAsync(long roadSignId);
}