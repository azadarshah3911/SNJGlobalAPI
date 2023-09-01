using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNJGlobalAPI.DbModelsProduction
{
    public class QA
    {
        [Key]
        public int ID { get; set; }
        public int? Fk_StatusId { get; set; }
        [ForeignKey("Fk_StatusId")]
        public Status Status { get; set; }

        public int? Fk_LeadID { get; set; }
        [ForeignKey("Fk_LeadID")]
        public Lead Lead { get; set; }
        public string Remarks { get; set; }

        public int? FK_CreatedBy { get; set; }
        [ForeignKey("FK_CreatedBy")]
        public User CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<QaQuestionAnswer> Answers { get; set; }
        public ICollection<QAFiles> Files { get; set; }
    }
}
