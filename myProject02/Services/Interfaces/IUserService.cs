
using myProject02.DTOs;

namespace myProject02.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        Task<UserDto?> GetUserByIdAsync(int id);

        Task<UserDto> CreateUserAsync(CreateUserDto dto);

        Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto dto);

        Task<bool> DeleteUserAsync(int id);

        Task<bool> UserExistsAsync(int id);
        Task<IEnumerable<UserDto>> GetUsersByRoleNameAsync(string roleName);
    }
}

