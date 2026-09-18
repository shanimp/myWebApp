using System.ComponentModel.DataAnnotations;

namespace myProject02.Dto
{
    public class CreateRoleDTO
    {
        [Required(ErrorMessage = "Role name is required.")]
        [StringLength(50, ErrorMessage = "Role name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;
    }
}
