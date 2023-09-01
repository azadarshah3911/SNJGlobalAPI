using System.Security.Principal;

namespace SNJGlobalAPI.DtoModelsProduction
{
    public class DashboardInputDto
    {
        public int BranchId { get; set; }
        public bool IsDaily { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }

    public class BranchLeadCountDto
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }

        public int NewLead { get; set; }
        public int SnsPending { get; set; }
        public int QaPending { get; set; }
        public int Processing { get; set; }
        public int EvError { get; set; }
        public int QaReExamine { get; set; }
        public int ChassingPending { get; set; }
        public int ChassingVerificationPending { get; set; }
        public int SnsFail { get; set; }

        public int Total { get; set; }
    }

    public class DailyAgentsReportDto
    {
        public int AgentId { get; set; }
        public string AgentName { get; set; }

        public int NewLead { get; set; }
        public int QaPending { get; set; }
        public int Processing { get; set; }
        public int EvError { get; set; }
        public int Total { get; set; }
        public int QaReExamine { get; set; }
        public int? Penalty { get; set; }
        public int? Bonus { get; set; }
        public int SnsFail { get; set; }
    }

    public class MonthlyAgentReportDto
    {
        public MonthlyAgentReportDto()
        {
            daywise = new();
        }
        public int AgentId { get; set; }
        public string AgentName { get; set; }

        public int TotalMonth { get; set; }
       
        public List<LeadCountDto>  days { get; set; }
        public List<DayWiseCountDto>  daywise { get; set; }
    }


    public class AgentMonthlyDetailReportDto
    {
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public List<DatesDto> NewLeadDay { get; set; } 
        public List<DatesDto> EvErrorDay { get; set; } 
        public List<DatesDto> QaPendingDay { get; set; } 
        public List<DatesDto> QaQualifiedDay { get; set; } 
        public List<DatesDto> NotQualifiedDay { get; set; } 
    }

    public class LeadCountDto
    {  
        public int Day { get; set; }  
    }
    
    public class DayWiseCountDto
    {  
        public int Day { get; set; }
        public int Count { get; set; }
    }

    public class DetailMonthDto
    {
        public int AgentId { get; set; }
        public string AgentName { get; set; }

        public List<MonthDayAgentDto> MonthDays { get; set; }
    }

    public class MonthDayAgentDto
    {
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public int NewLead { get; set; }
        public int EvError { get; set; }
        public int QaPending { get; set; }
        public int QaQualified { get; set; }
        public int NotQualified { get; set; }
        public int SnsFail { get; set; }
        public int Day { get; set; }
    }

    public class GetAssignedLeadDetailDto
    {
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public int Assigned { get; set; }
        public int Pending { get; set; }
        public int Complete { get; set; }
        public int Day { get; set; }
        public int LastDayPending { get; set; }
        public double Ratio { get => (LastDayPending + Assigned) / (Complete == 0 ? 1 : Complete); }
    }

    public class DatesDto
    {
        public DateTime? Date { get; set; }
    }

    public class GetMonthlyAgentReportDto
    {
        public int BranchId { get; set;}
        public int? Month { get; set;}
        public int? Year { get; set; }
    }

    public class MonthYearDto
    {
        public int? Month { get; set; }
        public int? Year { get; set; }
    }
    public class AgentIdDto : MonthYearDto
    {
        public int? AgentId { get; set; }
    }
}
