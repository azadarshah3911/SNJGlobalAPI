using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNJGlobalAPI.DbModelsProduction
{
    public class ChassingFile
    {

        [Key]
        public int Id { get; set; }
        public string File { get; set; }
        public string FileType { get; set; }

        public int? FK_LeadID { get; set; }
        [ForeignKey("FK_LeadID")]
        public Lead Lead { get; set; }
        public int? Fk_ChassingId { get; set; }
        [ForeignKey("Fk_ChassingId ")]
        public Chassing Chassing { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? FK_CreatedBy { get; set; }
        [ForeignKey("FK_CreatedBy")]
        public User CreatedBy { get; set; }
        public ICollection<ChassingFile> ChassingFiles { get; set; }

    }
}
