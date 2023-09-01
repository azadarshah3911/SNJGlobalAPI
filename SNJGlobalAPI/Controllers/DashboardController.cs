using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.Repositories.CommonInterfaces;

namespace SNJGlobalAPI.Controllers
{
    [Route("Production/dashbaord")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboard _dashboard;

        public DashboardController(IDashboard dashboard)
        {
            _dashboard = dashboard;
        }

        [HttpPost("BranchStatus")]
        [Authorize(Roles = $"{appRolesNameDto.TeamLead},{appRolesNameDto.ProductionManager},{appRolesNameDto.SuperAdmin}")]
        public async Task<IActionResult> BranchStatus(DashboardInputDto dto) =>
            Ok(await _dashboard.GetBranchCountAsync(dto));

        [HttpGet("DailyAssignedQaLeadsReport")]
        [Authorize(Roles = $"{appRolesNameDto.QaManager},{appRolesNameDto.QaAgent},{appRolesNameDto.SuperAdmin}")]
        public async Task<IActionResult> DailyAssignedQaLeadsReport() =>
            Ok(await _dashboard.GetDailyAssignedQaLeadReportAsync());

        [HttpPost("MonthlyAssignedQaLeadsReport")]
        [Authorize(Roles = $"{appRolesNameDto.QaManager},{appRolesNameDto.QaAgent},{appRolesNameDto.SuperAdmin}")]
        public async Task<IActionResult> MonthlyAssignedQaLeadsReport(MonthYearDto dto) =>
          Ok(await _dashboard.GetMonthlyAssignedQaLeadReportAsync(dto));

        [HttpGet("MonthlyAssignedQaLeadsDetailReport")]
        [Authorize(Roles = $"{appRolesNameDto.QaManager},{appRolesNameDto.QaAgent},{appRolesNameDto.SuperAdmin}")]
        public async Task<IActionResult> MonthlyAssignedQaLeadsDetailReport(int agentId) =>
         Ok(await _dashboard.GetMonthlyAssignedQaLeadDetailReportAsync(agentId));



        [HttpGet("DailyAssignedChassingLeadsReport")]
        [Authorize(Roles = $"{appRolesNameDto.ChassingAgent},{appRolesNameDto.ChassingManager},{appRolesNameDto.SuperAdmin}")]
        public async Task<IActionResult> DailyAssignedChassingLeadsReport() =>
           Ok(await _dashboard.GetDailyAssignedChassingLeadReportAsync());

        [HttpPost("MonthlyAssignedChassingLeadsReport")]
        [Authorize(Roles = $"{appRolesNameDto.ChassingAgent},{appRolesNameDto.ChassingManager},{appRolesNameDto.SuperAdmin}")]
        public async Task<IActionResult> MonthlyAssignedChassingLeadsReport(MonthYearDto dto) =>
         Ok(await _dashboard.GetMonthlyAssignedChassingLeadReportAsync(dto));

        [HttpGet("DailyAgentsReport")]
        public async Task<IActionResult> DailyAgentReport(int branchid) =>
            Ok(await _dashboard.GetAgentsDailyReportAsync(branchid));
         
        [HttpPost("MonthlyAgentsReport")]
        public async Task<IActionResult> MonthlyAgentReport(GetMonthlyAgentReportDto dto) =>
            Ok(await _dashboard.GetAgentsMonthlyReportAsync(dto));

        [HttpGet("DailyAgentStatus")]
        [Authorize(Roles = $"{appRolesNameDto.Agent}")]
        public async Task<IActionResult> DailyAgentStatus(int agentId) =>
            Ok(await _dashboard.GetAgentDailyCountAsync(agentId));

        [HttpGet("MonthlyAgentStatus")]
        [Authorize(Roles = $"{appRolesNameDto.Agent}")]
        public async Task<IActionResult> MonthlyAgentStatus(int agentId) =>
           Ok(await _dashboard.GetAgentMonthlyCountAsync(agentId));

        [HttpPost("GetLeadsForExcel")]
        public async Task<IActionResult> GetLeadsForExcel(int[] leadid) =>
           Ok(await _dashboard.GetExcelReportAsync(leadid));

        [HttpPost("GetLeadsPdfDetail")]
        public async Task<IActionResult> GetLeadsPdfDetail(int leadid) =>
         Ok(await _dashboard.GetPDFReportAsync(leadid));

        [HttpPost("GetAgentMonthlyDetailReport")]
        [Authorize(Roles = $"{appRolesNameDto.ProductionManager},{appRolesNameDto.TeamLead},{appRolesNameDto.SuperAdmin}")]
        public async Task<IActionResult> GetAgentMonthlyDetailReport(AgentIdDto dto) =>
         Ok(await _dashboard.GetAgentMonthlyDetailReportAsync(dto));
    }
}
