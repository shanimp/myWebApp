using myProject02.Dto;

namespace myProject02.Services
{
    public interface IRoleService
    {
        Task<RoleDto?> GetRoleByIdAsync(int id);
        Task<RoleDto?> CreateRoleAsync(CreateRoleDTO dto); 
    }
}
