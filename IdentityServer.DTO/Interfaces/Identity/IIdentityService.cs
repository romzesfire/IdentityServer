namespace IdentityServer.DTO.Interfaces.Identity;

public interface IIdentityService : IIdentityUserService, IIdentityRoleService
{
    Task<bool> IsInRoleAsync(string userId, string role);
    Task<List<string>> GetUserRolesAsync(string userId);
    Task<bool> AssignUserToRole(string userName, IList<string> roles);
    Task<bool> UpdateUsersRole(string userName, IList<string> usersRole);
}