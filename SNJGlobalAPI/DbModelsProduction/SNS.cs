using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DbModels
{
    public class SNS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [Display(Name = "SNS Check")]
        public int? FK_StatusID { get; set; }
        [ForeignKey("FK_StatusID")]
        public Status Status { get; set; }
        public string Remarks { get; set; }

        public int? FK_LeadID { get; set; }
        [ForeignKey("FK_LeadID")]
        public Lead Lead { get; set; }
        public int? FK_CreatedBy { get; set; }
        [ForeignKey("FK_CreatedBy")]
        public User CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public int? FK_UpdatedBy { get; set; }
        [ForeignKey("FK_UpdatedBy")]
        public User UpdateBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
