using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DbModels
{
    public class ForgetPassword
    {
        [Key]
        public Guid Id { get; set; }

        public int? Fk_UserId { get; set; }
        [ForeignKey("Fk_UserId")]
        public User user { get; set; }

        public DateTime RequestedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UsedOn { get; set; }
    }
}