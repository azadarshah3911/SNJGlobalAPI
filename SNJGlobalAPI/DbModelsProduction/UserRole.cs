using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNJGlobalAPI.DbModels
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        public int? Fk_UserId { get; set; }
        [ForeignKey("Fk_UserId")]
        public User user { get; set; }

        public int? Fk_RoleId { get; set; }
        [ForeignKey("Fk_RoleId")]
        public Role role { get; set; }

        public int? Fk_CreatedBy { get; set; }
        [ForeignKey("Fk_CreatedBy")]
        public User creator { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}