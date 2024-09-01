using AutoMapper;
using Domain.DorZnakUA.Dto.Role;
using Domain.DorZnakUA.Dto.UserRole;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Enum;
using Domain.DorZnakUA.Interfaces.Repositories;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Interfaces.Validations;
using Domain.DorZnakUA.Result;
using DorZnakUA.Application.Resources;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DorZnakUA.Application.Services;

public class RoleService : IRoleService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<Role> _roleRepository;
    private readonly IBaseRepository<UserRole> _userRoleRepository;
    private readonly IBaseValidator<User> _userValidator;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;


    public RoleService(IBaseRepository<User> userRepository, IBaseRepository<Role> roleRepository, IBaseRepository<UserRole> userRoleRepository, ILogger logger, IMapper mapper, IBaseValidator<User> userValidator)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _logger = logger;
        _mapper = mapper;
        _userValidator = userValidator;
    }

    /// <inheritdoc/>
    public async Task<BaseResult<RoleDto>> CreateRoleAsync(CreateRoleDto dto)
    {
        try
        {
            var role = await _roleRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Name == dto.Name);
            
            if (role!=null)
            {
                return new BaseResult<RoleDto>()
                {
                    ErrorMessage = ErrorMessage.RoleAlreadyExists,
                    ErroreCode = (int)ErrorCodes.RoleAlreadyExists,
                };
            }

            role = new Role()
            {
                Name = dto.Name
            };
            
            await _roleRepository.CreateAsync(role);
            await _roleRepository.SaveChangesAsync();

            return new BaseResult<RoleDto>()
            {
                Date = _mapper.Map<RoleDto>(role),
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<RoleDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int)ErrorCodes.ProjectsNotFound,
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<RoleDto>> DeleteRole(long id)
    {
        try
        {
            var role = await _roleRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (role == null)
            {
                return new BaseResult<RoleDto>()
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErroreCode = (int) ErrorCodes.RoleNotFound,
                };
            }

            var removeRole = _roleRepository.Remove(role);
            await _roleRepository.SaveChangesAsync();

            return new BaseResult<RoleDto>()
            {
                Date = _mapper.Map<RoleDto>(removeRole),
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<RoleDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int)ErrorCodes.ProjectsNotFound,
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<RoleDto>> UpdateRole(RoleDto dto)
    {
        try
        {
            var role = await _roleRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (role == null)
            {
                return new BaseResult<RoleDto>()
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErroreCode = (int) ErrorCodes.RoleNotFound,
                };
            }

            role.Name = dto.Name;
            var updateRole = _roleRepository.Update(role);
            await _roleRepository.SaveChangesAsync();

            return new BaseResult<RoleDto>()
            {
                Date = _mapper.Map<RoleDto>(updateRole),
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<RoleDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int)ErrorCodes.ProjectsNotFound,
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<UserRoleDto>> AddRoleForUserAsync(UserRoleDto dto)
    {
        try
        {
            var user = await _userRepository
                .GetAll()
                .Include(x=>x.Roles)
                .FirstOrDefaultAsync(x => x.Login == dto.Login);

            var result = _userValidator.ValidateOnNull(user);
            
            if (!result.IsSeccess)
            {
                return new BaseResult<UserRoleDto>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErroreCode = (int) ErrorCodes.UserNotFound,
                };
            }

            var roles = user.Roles
                .Select(x => x.Name)
                .ToArray();

            if (roles.All(x=>x != dto.RoleName))
            {
                var role = await _roleRepository
                    .GetAll()
                    .FirstOrDefaultAsync(x => x.Name == dto.RoleName);

                if (role==null)
                {
                    return new BaseResult<UserRoleDto>()
                    {
                        ErrorMessage = ErrorMessage.RoleNotFound,
                        ErroreCode = (int)ErrorCodes.RoleNotFound,
                    };
                }

                UserRole userRole = new UserRole()
                {
                    UserId = user.Id,
                    RoleId = role.Id
                };

                await _userRoleRepository.CreateAsync(userRole);
                await _userRoleRepository.SaveChangesAsync();

                return new BaseResult<UserRoleDto>()
                {
                    Date = new UserRoleDto(user.Login, role.Name)
                };
            }
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.UserAlreadyExistsThisRole,
                ErroreCode = (int) ErrorCodes.UserAlreadyExistsThisRole,
            };
        }
        
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<UserRoleDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int)ErrorCodes.ProjectsNotFound,
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<UserRoleDto>> DeleteRoleForUser(DeleteUserRoleDto dto)
    {
        return new BaseResult<UserRoleDto>()
        {

        };
    }

    /// <inheritdoc/>
    public async Task<BaseResult<UserRoleDto>> UpdateRoleForUse(UpdateUserRoleDto dto)
    {
        return new BaseResult<UserRoleDto>()
        {

        };
    }
}