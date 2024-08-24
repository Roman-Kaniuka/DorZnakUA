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
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public AuthService(IBaseRepository<User> userRepository, ILogger logger, IMapper mapper, 
        IBaseValidator<User> userValidator)
    {
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
        _userValidator = userValidator;
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
    public Task<BaseResult<TokenDto>> Login(LoginUserDto dto)
    {
        throw new NotImplementedException();
    }
    
    private string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}