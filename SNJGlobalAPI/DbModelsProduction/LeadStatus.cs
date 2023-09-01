using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNJGlobalAPI.DbModels
{
    public class LeadStatus
    {
        [Key]
        public int ID { get; set; }

        public int? FK_LeadId { get; set; }
        [ForeignKey("FK_LeadId")]
        public Lead lead { get; set; }

        public int? FK_StatusId { get; set; }
        [ForeignKey("FK_StatusId")]
        public Status status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? FK_CreatedBy { get; set; }
        [ForeignKey("FK_CreatedBy")]
        public User createdBy { get; set; }
    }
}