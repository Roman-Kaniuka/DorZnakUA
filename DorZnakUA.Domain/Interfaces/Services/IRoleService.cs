using Domain.DorZnakUA.Dto.Role;
using Domain.DorZnakUA.Dto.UserRole;
using Domain.DorZnakUA.Result;

namespace Domain.DorZnakUA.Interfaces.Services;

/// <summary>
/// Сервіс для роботи з ролями 
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// Створення нової ролі
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<RoleDto>> CreateRoleAsync(CreateRoleDto dto);

    /// <summary>
    /// Видаляє роль по id ролі
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<RoleDto>> DeleteRole(long id);

    /// <summary>
    /// Оновлення ролі
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<RoleDto>> UpdateRole(RoleDto dto);

    /// <summary>
    /// Присвоєння ролі користувачу
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<UserRoleDto>> AddRoleForUserAsync(UserRoleDto dto);

    /// <summary>
    /// Видалення ролі у користувача 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<UserRoleDto>> DeleteRoleForUser(DeleteUserRoleDto dto);

    /// <summary>
    /// Оновлення ролі у користувача
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<UserRoleDto>> UpdateRoleForUse(UpdateUserRoleDto dto);
}