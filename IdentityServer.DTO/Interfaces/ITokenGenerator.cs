namespace IdentityServer.DTO.Interfaces;

public interface ITokenGenerator
{
    public string GenerateJwtToken((string userId, string userName, IList<string> roles) userDetails);
    public string GenerateRefreshToken(string accessToken);
}