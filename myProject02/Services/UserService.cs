
using Microsoft.EntityFrameworkCore;
using myProject02.DTOs;
using myProject02.Models;
using myProject02.Services.Interfaces;

namespace myProject02.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // Get all users
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    RoleId = u.RoleId,
                    RoleName = u.Role.Name
                })
                .ToListAsync();
        }

        // Get user by ID
        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Id == id)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    RoleId = u.RoleId,
                    RoleName = u.Role.Name
                })
                .FirstOrDefaultAsync();
        }

        // Create user
        public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
        {
            // Check whether role exists
            var roleExists = await _context.Roles
                .AnyAsync(r => r.Id == dto.RoleId);

            if (!roleExists)
            {
                throw new ArgumentException("The specified RoleId does not exist.");
            }

            var user = new User
            {
                Name = dto.Name,
                RoleId = dto.RoleId
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            // Load role information
            await _context.Entry(user)
                .Reference(u => u.Role)
                .LoadAsync();

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                RoleId = user.RoleId,
                RoleName = user.Role.Name
            };
        }

        // Update user
        public async Task<UserDto?> UpdateUserAsync(
            int id,
            UpdateUserDto dto)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return null;
            }

            // Check whether new role exists
            var roleExists = await _context.Roles
                .AnyAsync(r => r.Id == dto.RoleId);

            if (!roleExists)
            {
                throw new ArgumentException("The specified RoleId does not exist.");
            }

            user.Name = dto.Name;
            user.RoleId = dto.RoleId;

            await _context.SaveChangesAsync();

            // Reload role after changing RoleId
            await _context.Entry(user)
                .Reference(u => u.Role)
                .LoadAsync();

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                RoleId = user.RoleId,
                RoleName = user.Role.Name
            };
        }

        // Delete user
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }

        // Check whether user exists
        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users
                .AnyAsync(u => u.Id == id);
        }
    }
}


