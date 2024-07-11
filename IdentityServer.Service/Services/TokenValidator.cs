using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;
using IdentityServer.Service.Extension;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.Service.Services;

public class TokenValidator : ITokenValidator
{
    public string? GetUserIdFromToken(string authToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ReadToken(authToken) as JwtSecurityToken;;
        var userId = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
        return userId;
    }
}