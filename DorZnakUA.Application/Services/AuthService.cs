using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Domain.DorZnakUA.Dto.Token;
using Domain.DorZnakUA.Dto.User;
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

public class AuthService : IAuthService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseValidator<User> _userValidator;
    private readonly IBaseRepository<Role> _roleRepository;
    private readonly IBaseRepository<UserToken> _userTokenRepository;
    private readonly IBaseRepository<UserRole> _userRoleRepository;
    private readonly ITokenService _tokenService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public AuthService(IBaseRepository<User> userRepository, ILogger logger, IMapper mapper, 
        IBaseValidator<User> userValidator, IBaseRepository<UserToken> userTokenRepository, 
        ITokenService tokenService, IBaseRepository<Role> roleRepository, IBaseRepository<UserRole> userRoleRepository)
    {
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
        _userValidator = userValidator;
        _userTokenRepository = userTokenRepository;
        _tokenService = tokenService;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
    }

    /// <inheritdoc/>
    public async Task<BaseResult<UserDto>> Register(RegisterUserDto dto)
    {
        if (dto.Password != dto.PasswordConfirm)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                ErroreCode = (int) ErrorCodes.PasswordNotEqualsPasswordConfirm,
            };
        }

        try
        {
            var user = await _userRepository
                        .GetAll()
                        .FirstOrDefaultAsync(x => x.Login == dto.Login);

            var result = _userValidator.ValidateOnNull(user);
            
            if (result.IsSeccess)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.UserAlreadyExists,
                    ErroreCode = (int) ErrorCodes.UserAlreadyExists,
                };
            }
            
            var hashUserPassword = HashPassword(dto.Password);

            user = new User()
            {
                Login = dto.Login,
                Password = hashUserPassword
            };
            
            await _userRepository.CreateAsync(user);
            await _userRepository.SaveChangesAsync();
            
            var role = await _roleRepository
                .GetAll()
                .FirstOrDefaultAsync(r => r.Name == (nameof(Roles.User)));
            
            if (role == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErroreCode = (int)ErrorCodes.RoleNotFound
                };
            }
            
            UserRole userRole = new UserRole()
            {
                UserId = user.Id,
                RoleId = role.Id
            };

            await _userRoleRepository.CreateAsync(userRole);
            await _userRoleRepository.SaveChangesAsync();

            return new BaseResult<UserDto>()
            {
                Date = _mapper.Map<UserDto>(user),
            };
        }
        catch (Exception e)
        {
            _logger.Error(e,e.Message);

            return new BaseResult<UserDto>()
            {
                ErrorMessage = ErrorMessage.RegistrationFailed,
                ErroreCode = (int) ErrorCodes.RegistrationFailed,
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<TokenDto>> Login(LoginUserDto dto)
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
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErroreCode = (int) ErrorCodes.UserNotFound,
                };
            }

            if (!IsVerifyPassword(user.Password, dto.Password))
            {
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = ErrorMessage.PasswordIsWrong,
                    ErroreCode = (int)ErrorCodes.PasswordIsWrong,
                };
            }

            var userToken = await _userTokenRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.UserId == user.Id);

            var userRoles = user.Roles;
            var claims = userRoles
                .Select(x => new Claim(ClaimTypes.Role, x.Name))
                .ToList();
            claims
                .Add(new Claim(ClaimTypes.Name, user.Login));
            
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            
            if (userToken==null)
            {
                userToken = new UserToken()
                {
                    UserId = user.Id,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7),
                };
                await _userTokenRepository.CreateAsync(userToken);
                await _userTokenRepository.SaveChangesAsync();
            }
            else
            {
                userToken.RefreshToken = refreshToken;
                userToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                
                _userTokenRepository.Update(userToken);
                await _userTokenRepository.SaveChangesAsync();
            }

            return new BaseResult<TokenDto>()
            {
                Date = new TokenDto(accessToken, refreshToken)
            };


        }
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<TokenDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErroreCode = (int)ErrorCodes.ProjectsNotFound,
            };
        }
        
    }
    
    private string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private bool IsVerifyPassword(string userPasswordHash, string userPassword)
    {
        var hash = HashPassword(userPassword);
        return userPasswordHash == hash;
    }
}