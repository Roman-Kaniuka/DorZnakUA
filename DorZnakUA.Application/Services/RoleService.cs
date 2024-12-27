using AutoMapper;
using Domain.DorZnakUA.Dto.Role;
using Domain.DorZnakUA.Dto.UserRole;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Enum;
using Domain.DorZnakUA.Interfaces.Repositories;
using Domain.DorZnakUA.Interfaces.Repositories.DateBases;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Interfaces.Validations;
using Domain.DorZnakUA.Result;
using DorZnakUA.Application.Resources;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DorZnakUA.Application.Services;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<Role> _roleRepository;
    private readonly IBaseRepository<UserRole> _userRoleRepository;
    private readonly IBaseValidator<User> _userValidator;
    private readonly IValidator<CreateRoleDto> _createRoleValidator;
    private readonly IValidator<RoleDto> _updateRoleValidator;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;


    public RoleService(IBaseRepository<User> userRepository, IBaseRepository<Role> roleRepository, 
        IBaseRepository<UserRole> userRoleRepository, ILogger logger, IMapper mapper, IBaseValidator<User> userValidator, 
        IUnitOfWork unitOfWork, IValidator<CreateRoleDto> createRoleValidator, IValidator<RoleDto> updateRoleValidator)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _logger = logger;
        _mapper = mapper;
        _userValidator = userValidator;
        _unitOfWork = unitOfWork;
        _createRoleValidator = createRoleValidator;
        _updateRoleValidator = updateRoleValidator;
    }

    /// <inheritdoc/>
    public async Task<BaseResult<RoleDto>> CreateRoleAsync(CreateRoleDto dto)
    {
        try
        {
            var validationResult = await _createRoleValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                _logger.Warning($"{validationResult}");
                return new BaseResult<RoleDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidInputDataError,
                    ErroreCode = (int) ErrorCodes.InvalidInputDataError,
                };
            }
            
            var role = await _roleRepository
                .GetAll()
                .AsNoTracking()
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
    public async Task<BaseResult<RoleDto>> DeleteRoleAsync(long id)
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
    public async Task<BaseResult<RoleDto>> UpdateRoleAsync(RoleDto dto)
    {
        try
        {
            var validationResult = await _updateRoleValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                _logger.Warning($"{validationResult}");
                return new BaseResult<RoleDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidInputDataError,
                    ErroreCode = (int)ErrorCodes.InvalidInputDataError,
                };
            }
            
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
                .AsNoTracking()
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
                    .AsNoTracking()
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
    public async Task<BaseResult<UserRoleDto>> DeleteRoleForUserAsync(DeleteUserRoleDto dto)
    {
        try
        {
            var user = await _userRepository
                .GetAll()
                .Include(x=>x.Roles)
                .AsNoTracking()
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
            
            var role = user.Roles.FirstOrDefault(x => x.Id == dto.RoleId);

            if (role==null)
            {
                return new BaseResult<UserRoleDto>()
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErroreCode = (int)ErrorCodes.RoleNotFound,
                };
            }

            var userRole = await _userRoleRepository
                .GetAll()
                .Where(x => x.RoleId == role.Id)
                .FirstOrDefaultAsync(x => x.UserId == user.Id);

            _userRoleRepository.Remove(userRole);
            await _userRoleRepository.SaveChangesAsync();

            return new BaseResult<UserRoleDto>()
            {
                Date = new UserRoleDto(user.Login, role.Name)
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
    public async Task<BaseResult<UserRoleDto>> UpdateRoleForUseAsync(UpdateUserRoleDto dto)
    {
        try
        {
            var user = await _userRepository
                .GetAll()
                .Include(x=>x.Roles)
                .AsNoTracking()
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

            var role = user.Roles.FirstOrDefault(x => x.Id == dto.FromRoleId);
            
            if (role == null)
            {
                return new BaseResult<UserRoleDto>()
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErroreCode = (int)ErrorCodes.RoleNotFound
                };
            }

            var newRoleForUser = await _roleRepository
                .GetAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == dto.ToRoleId);

            if (newRoleForUser==null)
            {
                return new BaseResult<UserRoleDto>()
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErroreCode = (int)ErrorCodes.RoleNotFound
                };
            }

            var oldRoleForUser = await _userRoleRepository
                .GetAll()
                .Where(x => x.RoleId == role.Id)
                .FirstOrDefaultAsync(x => x.UserId == user.Id);

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    _unitOfWork.UserRoles.Remove(oldRoleForUser);
                    await _unitOfWork.SaveChangesAsync();

                    var newUserRole = new UserRole()
                    {
                        UserId = user.Id,
                        RoleId = newRoleForUser.Id
                    };
                    await _unitOfWork.UserRoles.CreateAsync(newUserRole);
                    await _unitOfWork.SaveChangesAsync();
                    
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                }
            }
            return new BaseResult<UserRoleDto>()
            {
                Date = new UserRoleDto(user.Login, newRoleForUser.Name)
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
}