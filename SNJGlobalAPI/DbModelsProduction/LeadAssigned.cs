using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNJGlobalAPI.DbModelsProduction
{
    public class LeadAssigned
    {
        [Key]
        public int Id { get; set; }

        public int FK_LeadId { get; set; }
        [ForeignKey("FK_LeadId")]
        public Lead Lead { get; set; }

        public int? FK_AgentId { get; set; }
        [ForeignKey("FK_AgentId")]
        public User Agent { get; set; }

        public int? FK_SupervisorId { get; set; }
        [ForeignKey("FK_SupervisorId")]
        public User Supervisor { get; set; }

        public int FK_StageId { get; set; }
        [ForeignKey("FK_StageId")]
        public Stage Stage { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
