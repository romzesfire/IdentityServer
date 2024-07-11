namespace IdentityServer.Service.Services;

public interface ITokenValidator
{
    string? GetUserIdFromToken(string authToken);
}