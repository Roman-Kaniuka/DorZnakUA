using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.DorZnakUA.Dto.Token;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Interfaces.Repositories;
using Domain.DorZnakUA.Interfaces.Services;
using Domain.DorZnakUA.Result;
using Domain.DorZnakUA.Settings;
using DorZnakUA.Application.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DorZnakUA.Application.Services;

public class TokenService : ITokenService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly string _jwtKey;
    private readonly string _issuer;
    private readonly string _audience;

    public TokenService(IOptions<JwtSettings> options, IBaseRepository<User> userRepository)
    {
        _userRepository = userRepository;
        _jwtKey = options.Value.JwtKey;
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
    }
    
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var securityToken = new JwtSecurityToken(_issuer,_audience, claims, null , DateTime.Now.AddMinutes(10),credentials);
        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }

    public string GenerateRefreshToken()
    {
        var randomNumbers = new byte [32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumbers);
        return Convert.ToBase64String(randomNumbers);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
            ValidateLifetime = true,
            ValidAudience = _audience,
            ValidIssuer = _issuer
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var claimsPrincipal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityException(ErrorMessage.InvalidToken);
        } ;
        return claimsPrincipal;
    }

    public async Task<BaseResult<TokenDto>> RefreshTokenAsync(TokenDto dto)
    {
        var accessToken = dto.AccessToken;
        var refreshToken = dto.RefreshToken;

        var claimsPrincipal = GetPrincipalFromExpiredToken(accessToken);
        var userName = claimsPrincipal.Identity?.Name;

        var user = await _userRepository
            .GetAll()
            .Include(x => x.UserToken)
            .FirstOrDefaultAsync(u => u.Login == userName);
        if (user == null || user.UserToken.RefreshToken != refreshToken ||
            user.UserToken.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return new BaseResult<TokenDto>()
            {
                ErrorMessage = ErrorMessage.InvslidClientRequest
            };
        }

        var newAccessToken = GenerateAccessToken(claimsPrincipal.Claims);
        var newRefreshToken = GenerateRefreshToken();

        user.UserToken.RefreshToken = newRefreshToken;
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();

        return new BaseResult<TokenDto>()
        {
            Date = new TokenDto(newAccessToken, newRefreshToken)
        };
    }
}