namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IRoleService
    {
        Dictionary<string, string> GetAllRoles();
        Task<(string id, string name)> GetRoleById(string id);
        Task<bool> CreateRole(string name);
        Task<bool> DeleteRole(string name);
        Task<bool> UpdateRole(string id,string name);
    }
}
