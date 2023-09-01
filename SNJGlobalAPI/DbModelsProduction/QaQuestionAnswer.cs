using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNJGlobalAPI.DbModelsProduction
{
    public class QaQuestionAnswer
    {
        [Key]
        public int ID { get; set; }

        public int? FK_QaID { get; set; }
        [ForeignKey("FK_QaID")]
        public QA Qa { get; set; }

        public int? FK_QuestionID { get; set; }
        [ForeignKey("FK_QuestionID")]
        public ProductQuestion Question { get; set; }

        public string Answer { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? FK_CreatedBy { get; set; }
        [ForeignKey("FK_CreatedBy")]
        public User CreatedBy { get; set; }

    }
}
