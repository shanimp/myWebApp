using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace myProject02.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // Foreign key
        public int RoleId { get; set; }

        // Navigation property
        public Role Role { get; set; } = null!;
    }
}
