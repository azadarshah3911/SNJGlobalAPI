using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNJGlobalAPI.DbModelsProduction
{
    public class Chassing
    {
        [Key]
        public int Id { get; set; }

        public int? Fk_LeadId { get; set; }
        [ForeignKey("Fk_LeadId")]
        public Lead Lead { get; set; }

        public int? Fk_StatusId { get; set; }
        [ForeignKey("Fk_StatusId")]
        public Status Status { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? FK_CreatedBy { get; set; }
        [ForeignKey("FK_CreatedBy")]
        public User CreatedBy { get; set; }

        public ICollection<ChassingFile> Files { get; set; }
    }
}
