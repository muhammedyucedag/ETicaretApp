using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Domain.Entites.Identity;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Persistence.Services
{
    internal class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRole(string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new() { Name = name });
            return result.Succeeded;
        }

        public async Task<bool> DeleteRole(string name)
        {
            IdentityResult result = await _roleManager.DeleteAsync(new() { Name = name });
            return result.Succeeded;
        }

        public Dictionary<string, string> GetAllRoles()
        {
            return _roleManager.Roles.ToDictionary(role => role.Id, role => role.Name);
        }

        public async Task<(string id, string name)> GetRoleById(string id)
        {
            string role = await _roleManager.GetRoleIdAsync(new() { Id = id});
            return (id, role);
        }

        public async Task<bool> UpdateRole(string id,string name)
        {
            IdentityResult result = await _roleManager.UpdateAsync(new() {Id = id, Name = name });
            return result.Succeeded;
        }
    }
}
