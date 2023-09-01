using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DbModels
{
    public class AgentPenalty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Amount { get; set; } = 0;

        public string Reason { get; set; }
        public int? Fk_StageId { get; set; }
        [ForeignKey("Fk_StageId")]
        public Stage Stage { get; set; }
        
        public int? Fk_LeadID { get; set; }
        [ForeignKey("Fk_LeadID")]
        public Lead Lead { get; set; }
        
        public int? Fk_PenaltyTo { get; set; }
        [ForeignKey("Fk_PenaltyTo")]
        public User PenaltyTo { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public int? Fk_PenaltyFrom { get; set; }
        [ForeignKey("Fk_PenaltyFrom")]
        public User PenaltyFrom { get; set; }
    }
}
