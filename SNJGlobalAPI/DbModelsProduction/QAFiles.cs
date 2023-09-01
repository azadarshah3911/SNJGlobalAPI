using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DbModelsProduction
{
    public class QAFiles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? FK_QAId { get; set; }
        [ForeignKey("FK_QAId")]
        public QA QA { get; set; }
        
        public string File { get; set; }
        public string FileType { get; set; }
       
        public int? CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
