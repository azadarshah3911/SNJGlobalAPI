using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DbModelsProduction
{
    public class Confirmation
    {
        [Key]
        public int Id { get; set; }

        public string Notes { get; set; }

        public int? Fk_LeadId { get; set; }
        [ForeignKey("Fk_LeadId")]
        public Lead Lead { get; set; }

        public int? Fk_StatusId { get; set; }
        [ForeignKey("Fk_StatusId")]
        public Status Status { get; set; }

        public int? Fk_UserId { get; set; }
        [ForeignKey("Fk_UserId")]
        public User CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
