using SNJGlobalAPI.DbModelsProduction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNJGlobalAPI.DbModels
{
    public class Status
    {
        [Key]
        public int ID { get; set; }

        public int? Fk_StageId { get; set; }
        [ForeignKey("Fk_StageId")]
        public Stage Stage { get; set; }

        [Required,StringLength(50)]
        public string Name { get; set; }

        public ICollection<Eligibility> Eligibilities { get; set; }
        public ICollection<LeadStatus> leadStatuses { get; set; }
        public ICollection<Lead> Lead { get; set; }
        public ICollection<SNS> SNS { get; set; }
        public ICollection<QA> QA { get; set; }
        public ICollection<Chassing> Chassings { get; set; }
        public ICollection<ChassingVerification> ChassingVerifications { get; set; }
        public ICollection<Confirmation> Confiramtions { get; set; }
    }
}
