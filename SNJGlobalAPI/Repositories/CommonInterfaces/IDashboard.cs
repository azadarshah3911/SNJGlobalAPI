using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.CommonInterfaces
{
    public interface IDashboard
    {
        Task<Responder<BranchLeadCountDto>> GetBranchCountAsync(DashboardInputDto dto);
        Task<Responder<List<GetDailyAssignedLeads>>> GetDailyAssignedQaLeadReportAsync();
        Task<Responder<List<GetDailyAssignedLeads>>> GetMonthlyAssignedQaLeadReportAsync(MonthYearDto dto);
        Task<Responder<List<GetAssignedLeadDetailDto>>> GetMonthlyAssignedQaLeadDetailReportAsync(int agentId);
        Task<Responder<List<GetDailyAssignedLeads>>> GetDailyAssignedChassingLeadReportAsync();
        Task<Responder<List<GetDailyAssignedLeads>>> GetMonthlyAssignedChassingLeadReportAsync(MonthYearDto dto);
        Task<Responder<DailyAgentsReportDto>> GetAgentDailyCountAsync(int agentId);
        Task<Responder<DailyAgentsReportDto>> GetAgentMonthlyCountAsync(int agentId);
        
        Task<Responder<List<DailyAgentsReportDto>>> GetAgentsDailyReportAsync(int branchid);
        Task<Responder<List<MonthlyAgentReportDto>>> GetAgentsMonthlyReportAsync(GetMonthlyAgentReportDto branchid);
        Task<Responder<List<LeadFullDetailListDto>>> GetExcelReportAsync(int[] leadid);
        Task<Responder<PdfDto>> GetPDFReportAsync(int leadid);

        Task<Responder<List<MonthDayAgentDto>>> GetAgentMonthlyDetailReportAsync(AgentIdDto dto);

    }
}
