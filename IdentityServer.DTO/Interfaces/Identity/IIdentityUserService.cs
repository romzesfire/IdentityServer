namespace IdentityServer.DTO.Interfaces.Identity;

public interface IIdentityUserService
{
    Task<(bool isSucceed, string userId)> CreateUserAsync(string userName, string password, string confirmationPassword, string email, List<string> roles);
    Task<bool> SigninUserAsync(string userName, string password);
    Task<string> GetUserIdAsync(string userName);
    Task<(string userId, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string userId);
    Task<(string userId, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName);
    Task<string> GetUserNameAsync(string userId);
    Task<bool> DeleteUserAsync(string userId);
    Task<bool> IsUniqueUserName(string userName);
    Task<List<(string id, string userName, string email)>> GetAllUsersAsync();
    Task<List<(string id, string userName, string email, IList<string> roles)>> GetAllUsersDetailsAsync();
    Task<bool> UpdateUserProfile(string id, string email, IList<string> roles);
}