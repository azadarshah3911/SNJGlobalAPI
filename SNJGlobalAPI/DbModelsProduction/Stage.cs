using SNJGlobalAPI.DbModelsProduction;
using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DbModels
{
    public class Stage
    {
        [Key]
        public int ID { get; set; }

        [StringLength(30),Required]
        public string Name { get; set; }
        public int? StageNo { get; set; }

        public ICollection<Status> Statuses { get; set; }
        public ICollection<LeadProduct> LeadProducts { get; set; }
        public ICollection<LeadSubProduct> LeadSubProducts { get; set; }
        public ICollection<LeadComments> LeadComments { get; set; }
        public ICollection<LeadFile> LeadFiles { get; set; }
        public ICollection<AgentPenalty> AgentPenalties { get; set; }
        public ICollection<ProductQuestion> ProductQuestions { get; set; }
        public ICollection<LeadAssigned> LeadAssigneds { get; set; }
        public ICollection<PatientLogs> PatientLogs { get; set; }
    }
}
