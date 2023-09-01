using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DbModels
{
    public class UserBonus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Amount { get; set; }
        public int? Fk_BonusTo { get; set; }
        [ForeignKey("Fk_BonusTo")]
        public User BonusTo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? Fk_BonusFrom { get; set; }
        [ForeignKey("Fk_BonusFrom")]
        public User BonusFrom { get; set; }
    }
}
