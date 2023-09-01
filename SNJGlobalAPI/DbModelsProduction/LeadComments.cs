using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DbModels
{
    public class LeadComments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Comment { get; set; }
        public int? Fk_StageId { get; set; }
        [ForeignKey("Fk_StageId")]
        public Stage Stage { get; set; }
        public int? FK_LeadID { get; set; }
        [ForeignKey("FK_LeadID")]
        public Lead Lead { get; set; }
        public int? FK_CreatedBy { get; set; }
        [ForeignKey("FK_CreatedBy")]
        public User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
