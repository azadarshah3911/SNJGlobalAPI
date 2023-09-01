using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using SNJGlobalAPI.DbModelsProduction;

namespace SNJGlobalAPI.DbModels
{
    public class Lead
    {
        [Key]
        public int ID { get; set; }
        public int? Fk_ParentId { get; set; }
        [ForeignKey("Fk_ParentId")]
        public Lead Parent { get; set; }

        public int? Fk_PatientId { get; set; }
        [ForeignKey("Fk_PatientId")]
        public Patient Patient { get; set; }

        public int? Fk_StatusId { get; set; }
        [ForeignKey("Fk_StatusId")]
        public Status Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; }
        public int? FK_CreatedBy { get; set; }
        [ForeignKey("FK_CreatedBy")]
        public User CreatedBy { get; set; }
        public bool IsDuplicate { get; set; }
        public string OnBehalf { get; set; }
        public ICollection<Lead> ParentLeads { get; set; }
        public ICollection<LeadSubProduct> LeadSubProducts { get; set; }
        public ICollection<LeadProduct> LeadProducts { get; set; }
        public ICollection<Eligibility> Eligibilities { get; set; }
        public ICollection<LeadFile> LeadFiles { get; set; }
        public ICollection<LeadComments> LeadComments { get; set; }
        public ICollection<PatientLogs> LeadLogs { get; set; }
        public ICollection<LeadStatus> leadStatuses { get; set; }
        public ICollection<ProductQuestionAnswer> ProductQuestionAnswer { get; set; }
        public ICollection<AgentPenalty> AgentPenalties { get; set; }
        public ICollection<LeadAssigned> LeadAssigneds { get; set; }
        public ICollection<Chassing> Chassings { get; set; }
        public ICollection<ChassingFile> ChassingFiles { get; set; }
        /* public ICollection<AgentPenalty> AgentPenalties { get; set; }*/
        public SNS SNS { get; set; }
        public ICollection<QA> QA { get; set; }
        public ChassingVerification ChassingVerification { get; set; }
        public Confirmation Confirmation { get; set; }



        public int? FK_DeletedBy { get; set; }
        [ForeignKey("FK_DeletedBy")]
        public User DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
