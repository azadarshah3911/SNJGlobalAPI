using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DbModels
{
    public class Role
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public string Name { get; set; }

        public ICollection<UserRole> userRoles { get; set; }
    }
}
