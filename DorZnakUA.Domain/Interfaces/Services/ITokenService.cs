using System.Security.Claims;
using Domain.DorZnakUA.Dto.Token;
using Domain.DorZnakUA.Result;

namespace Domain.DorZnakUA.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);
    
    Task<BaseResult<TokenDto>> RefreshTokenAsync(TokenDto dto);
}