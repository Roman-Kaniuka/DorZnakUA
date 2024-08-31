using System.Net.Mime;
using Asp.Versioning;
using Domain.DorZnakUA.Dto.Role;
using Domain.DorZnakUA.Dto.UserRole;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using Microsoft.AspNetCore.Mvc;

namespace DorZnakUA.Api.Controller;

/// <summary>
/// Сервіс для роботи з ролями
/// </summary>
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// Створення ролі
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST
    ///     {
    ///         "name": "Admin",
    ///     }
    /// </remarks>
    /// <response code="200">Якщо роль було створено</response>
    /// <response code="400">Якщо роль не було створено</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<RoleDto>>> CreateRole([FromBody]CreateRoleDto dto)
    {
        var response = await _roleService.CreateRoleAsync(dto);
        
        if (response.IsSeccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// Видалення ролі по id
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE
    ///     {
    ///         "id": "1"
    ///     }
    /// </remarks>>
    /// <response code="200">Якщо роль було видалено</response>
    /// <response code="400">Якщо роль не було видалено</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<RoleDto>>> DeleteRole(long id)
    {
        var response = await _roleService.DeleteRole(id);
        
        if (response.IsSeccess)
        {
            return Ok(response);
        }
        
        return BadRequest(response);
    }

    /// <summary>
    /// Оновлення ролі
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT
    ///     {
    ///         "id": "1",
    ///         "name": "Admin",
    ///     }
    /// </remarks>
    /// <response code="200">Якщо роль було оновлено</response>
    /// <response code="400">Якщо роль не було оновлено</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<RoleDto>>> UpdateRole([FromBody]RoleDto dto)
    {
        var response = await _roleService.UpdateRole(dto);
        
        if (response.IsSeccess)
        {
            return Ok(response);
        }
        
        return BadRequest(response);
    }
    
    /// <summary>
    /// Присвоєння ролі користувачу
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST
    ///     {
    ///         "login": "User #1",
    ///         "roleName": "Admin"
    ///     }
    /// </remarks>
    /// <response code="200">Якщо роль було присвоєно</response>
    /// <response code="400">Якщо роль не було присвоєно</response>
    [HttpPost("addRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> AddRoleForUser([FromBody]UserRoleDto dto)
    {
        var response = await _roleService.AddRoleForUserAsync(dto);
        if (response.IsSeccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}