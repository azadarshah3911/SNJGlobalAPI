using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DbModels
{
    public class UserLogin
    {
        [Key]
        public int Id { get; set; }

        public int Fk_UserId { get; set; }
        [ForeignKey("Fk_UserId")]
        public User user { get; set; }

        public DateTime Dated { get; set; } = DateTime.UtcNow;
        public bool IsLoggedIn { get; set; }
    }
}