namespace IdentityServer.DTO.Interfaces.Identity;

public interface IIdentityRoleService
{
    Task<bool> CreateRoleAsync(string roleName);
    Task<bool> DeleteRoleAsync(string roleId);
    Task<List<(string id, string roleName)>> GetRolesAsync();
    Task<(string id, string roleName)> GetRoleByIdAsync(string id);
    Task<bool> UpdateRole(string id, string roleName);
}