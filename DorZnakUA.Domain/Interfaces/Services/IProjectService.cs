using Domain.DorZnakUA.Dto.Project;
using Domain.DorZnakUA.Result;


namespace Domain.DorZnakUA.Interfaces.Services;

/// <summary>
/// Сервіс відповідає за роботу з доменною частиною проєкта (Project)
/// </summary>
public interface IProjectService
{
    /// <summary>
    /// Отримання всіх проєктів користувача
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<CollectionResult<ProjectDto>> GetProjectsAsync(long userId);
}