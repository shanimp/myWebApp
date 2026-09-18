using System.ComponentModel.DataAnnotations;

namespace myProject02.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
