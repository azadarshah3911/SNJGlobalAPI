using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DbModelsProduction
{
    public class ProductQuestionAnswer
    {
        [Key]
        public int ID { get; set; }
        public int? FK_LeadId { get; set; }
        [ForeignKey("FK_LeadId")]
        public Lead Lead { get; set; }
        public int? FK_QuestionId { get; set; }
        [ForeignKey("FK_QuestionId")]
        public ProductQuestion ProductQuestion { get; set; }
        public string Answer { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
        public int? FK_CreatedBy { get; set; }
        [ForeignKey("FK_CreatedBy")]
        public User User { get; set; }

    }
}
