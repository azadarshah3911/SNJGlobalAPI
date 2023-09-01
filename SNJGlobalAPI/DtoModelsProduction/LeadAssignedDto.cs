using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNJGlobalAPI.DtoModelsProduction
{
    public class AddLeadAssignedDto
    {
        public List<int> FK_LeadId { get; set; }
        public int FK_AgentId { get; set; }
    }

    public class GetDailyAssignedLeads
    {
        public int  AgentId { get; set; }
        public string AgentName { get; set; }
        public string Branch { get; set; }
        public int Assigned { get; set; }
        public int Pending { get; set; }
        public int Complete { get; set; } 
        public int LastDayPending { get; set; }
        public double Ratio { get => (LastDayPending + Assigned) / (Complete == 0 ? 1 : Complete); }

    }
    

    public class GetLeadAssignedDto
    {
        public int FK_LeadId { get; set; }
        public int FK_StageId { get; set; }
    }
    public class SwipLeadAssignedDto
    {
        public List<int> FK_LeadId { get; set; }
        public int FK_StageId { get; set; }
    }
}
