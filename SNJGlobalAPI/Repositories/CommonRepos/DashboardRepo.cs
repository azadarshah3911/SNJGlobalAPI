using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DbModelsProduction;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.GeneralServices;
using SNJGlobalAPI.Mappers;
using SNJGlobalAPI.Repositories.CommonInterfaces;
using SNJGlobalAPI.SecurityHandlers;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace SNJGlobalAPI.Repositories.CommonRepos;
public class DashboardRepo : IDashboard
{
    private readonly IDb _db;
    private readonly HttpContext httpContext;

    public DashboardRepo(IDb db, IHttpContextAccessor accessor)
    {
        _db = db;
        httpContext = accessor.HttpContext;
    }

    //daily/monthly branch status
    public async Task<Responder<BranchLeadCountDto>> GetBranchCountAsync(DashboardInputDto dto)
    {
        Expression<Func<Lead, bool>> condition = null;
        Expression<Func<QA, bool>> conditionqa = null;
        if (dto.IsDaily)
        {
            if (dto.BranchId > 0)
            {
                condition = w => w.CreatedBy.Fk_BranchId == dto.BranchId && w.CreatedAt.Date == DateTime.UtcNow.Date;
                conditionqa = w => w.CreatedBy.Fk_BranchId == dto.BranchId && w.Lead.CreatedAt.Date == DateTime.UtcNow.Date;
            }
            else
            {
                condition = w => w.CreatedAt.Date == DateTime.UtcNow.Date;
                conditionqa = w => w.Lead.CreatedAt.Date == DateTime.UtcNow.Date;
            }

        }
        else
        {
            DateTime currentDate = DateTime.UtcNow.Date;
            DateTime startDate = new DateTime(dto.Year == 0 ? currentDate.Year : dto.Year, dto.Month == 0 ? currentDate.Month : dto.Month , 1);
            DateTime endDate = startDate.AddMonths(1);
            if (dto.BranchId > 0)
            {
                condition = w => w.CreatedBy.Fk_BranchId == dto.BranchId && w.CreatedAt.Date >= startDate && w.CreatedAt < endDate;
                conditionqa = w => w.Lead.CreatedBy.Fk_BranchId == dto.BranchId && w.Lead.CreatedAt.Date >= startDate && w.Lead.CreatedAt < endDate;

            }
            else
            {
                condition = w => w.CreatedAt.Date >= startDate && w.CreatedAt < endDate;
                conditionqa = w => w.Lead.CreatedAt.Date >= startDate && w.Lead.CreatedAt < endDate;
            }
        }


        var data = await _db.Db().Leads
            .Where(condition)
            .GroupBy(g => g.CreatedBy.Fk_BranchId)
            .Select(s => new BranchLeadCountDto
            {
                BranchId = s.First().CreatedBy.branch.ID,
                BranchName = s.First().CreatedBy.branch.Name,
                NewLead = s.Where(w => w.Fk_StatusId == 1).Count(),
                EvError = s.Where(w => w.Fk_StatusId == 4).Count(),
                QaPending = s.Where(w => w.Fk_StatusId == 3).Count(),
                SnsPending = s.Where(w => w.Fk_StatusId == 2).Count(),
                QaReExamine = s.Where(w => w.Fk_StatusId == 18).Count(),
                ChassingPending = s.Where(w => w.Fk_StatusId == 15).Count(),
                ChassingVerificationPending = s.Where(w => w.Fk_StatusId == 28).Count(),
                SnsFail = s.Where(w => w.Fk_StatusId == 19).Count(),
                //  Total = s.Where(w => w.Fk_StatusId == 1 || w.Fk_StatusId == 4 || w.Fk_StatusId == 3 || w.Fk_StatusId == 15 || w.Fk_StatusId == 2 || w.Fk_StatusId == 18).Count(),
            }).AsNoTracking().AsSplitQuery().FirstOrDefaultAsync();
        var process = _db.Db().QAs.Include(l => l.Lead).Where(conditionqa).AsNoTracking().AsSplitQuery().Count(c => c.Fk_StatusId == 17);
        //data.Processing = process;  
        if (data is null)
            data = new()
            {
                Processing = process,
            };
        else
            data.Processing = process;
        return Rr.SuccessFetch(data);
    }

    //for admin to view all todays agents statuses
    public async Task<Responder<List<DailyAgentsReportDto>>> GetAgentsDailyReportAsync(int branchid)
    {
        var data = await _db.GetAllByAsync<User, DailyAgentsReportDto>
        (
            DashboardMapper.DailyAgentsListReport(), w => w.Fk_BranchId == branchid
        );
        if (data is null) return Rr.NoData(data);
        return Rr.SuccessFetch(data);
    }

    public async Task<Responder<List<MonthlyAgentReportDto>>> GetAgentsMonthlyReportAsync(GetMonthlyAgentReportDto dto)
    {
        var crnt = DateTime.Now;

        var startMonth = new DateTime(dto.Year == null ? crnt.Year : dto.Year ?? 0, dto.Month == null ? crnt.Month : dto.Month ?? 0, 1);
        var endMonth = startMonth.AddMonths(1);

        //get dates against each agent
        var data = await _db.GetAllByAsync<User, MonthlyAgentReportDto>
        (
            DashboardMapper.MonthlyAgentListReport(dto.Month ?? 0 ,dto.Year ?? 0),
            w => w.Fk_BranchId == dto.BranchId &&
            w.Lead.Any(a => a.CreatedAt.Date >= startMonth && a.CreatedAt.Date < endMonth)
        );

        //count datewise total no of leads for each agent
        List<MonthlyAgentReportDto> agents = new();
        foreach (var item in data)
        {
            //swap initial data
            MonthlyAgentReportDto month = new()
            {
                AgentId = item.AgentId,
                AgentName = item.AgentName,
                TotalMonth = item.TotalMonth
            };
            //group by day and count
            foreach (var group in item.days.GroupBy(g => g.Day))
            {
                month.daywise.Add(new()
                {
                    Day = group.Key,
                    Count = group.Count()
                });
            }//inner foreach group

            agents.Add(month);
        }//outer foreach data

        //add missing days
        //foreach (var agent in agents)
        //{
        //    foreach (var day in agent.daywise)
        //    {
        //        day.da
        //    }
        //}

        if (data is null) return Rr.NoData(agents);
        return Rr.SuccessFetch(agents);
    }

    public async Task<Responder<List<GetDailyAssignedLeads>>> GetDailyAssignedQaLeadReportAsync()
    {
        var data = await _db.GetAllByAsync<User, GetDailyAssignedLeads>
        (
            DashboardMapper.DailyAssignQaLeadsReport(), w => /*w.LeadAssignedsTo.Any(a => a.CreatedAt.Date == DateTime.UtcNow.Date)
            &&*/ w.usersRoles.Any(a => a.Fk_RoleId == 5)

        );
        if (data is null) return Rr.NoData(data);
        return Rr.SuccessFetch(data);
    }


    public async Task<Responder<List<GetDailyAssignedLeads>>> GetMonthlyAssignedQaLeadReportAsync(MonthYearDto dto)
    {
        var date = dto.Month == null ? DateTime.UtcNow.Month : dto.Month;
        var dateYear = dto.Year == null ? DateTime.UtcNow.Year : dto.Year;
        var data = await _db.GetAllByAsync<User, GetDailyAssignedLeads>
        (
            DashboardMapper.MonthlyAssignQaLeadsReport(dto), w => w.LeadAssignedsTo.Any(a => a.CreatedAt.Month == date && a.CreatedAt.Year == dateYear)
            && w.usersRoles.Any(a => a.Fk_RoleId == 5)
        );
        if (data is null) return Rr.NoData(data);
        return Rr.SuccessFetch(data);
    }

    public async Task<Responder<List<GetAssignedLeadDetailDto>>> GetMonthlyAssignedQaLeadDetailReportAsync(int agentId)
    {
        int? createdBy = agentId == 0 ? JwtHandlerRepo.GetCrntUserId(httpContext) : agentId;
        var crnt = DateTime.UtcNow;
        var startMonth = new DateTime(crnt.Year, crnt.Month, 1);
        var endMonth = startMonth.AddMonths(1);

        var data = await _db.Db().Users.Include(property => property.LeadAssignedsTo)
                             .Include(property => property.Lead).ThenInclude(property => property.QA)
                             .Where(predicate => predicate.ID == agentId
                                 && (predicate.LeadAssignedsTo.Any(anyPredicate => anyPredicate.CreatedAt >= startMonth && predicate.CreatedAt <= endMonth) && predicate.usersRoles.Any(an => an.Fk_RoleId == 5)))
                            .Select(selector => new GetAssignedLeadDetailDto()
                             {
                                 AgentId = selector.ID,
                                 AgentName = selector.FirstName + " " + selector.LastName,
                                 Day = selector.LeadAssignedsTo.FirstOrDefault(first => first.FK_StageId == 4).CreatedAt.Day,
                                 Assigned = selector.LeadAssignedsTo.Where(an => an.FK_StageId == 4 && selector.LeadAssignedsTo.FirstOrDefault().CreatedAt.Date == selector.LeadAssignedsTo.FirstOrDefault().CreatedAt.Date).Count(),
                                 //Pending = selector.LeadAssignedsTo.Where(countPredicate => countPredicate.FK_StageId == 4 && selector.Lead.Where(an => an.QA.FirstOrDefault() == null && countPredicate.CreatedAt.Date == selector.CreatedAt.Date)).Count(),
                                 //Complete = selector.Where(countPredicate => countPredicate.LeadAssignedsTo.Any(pred => pred.FK_StageId == 4) && countPredicate.Lead.Any(an => an.QA.FirstOrDefault() != null && countPredicate.CreatedAt.Date == selector.FirstOrDefault().CreatedAt.Date)).Count(),
                                 //LastDayPending = selector.Where(countPredicate => countPredicate.LeadAssignedsTo.Any(pred => pred.FK_StageId == 4 && countPredicate.Lead.Any(an => an.QA.FirstOrDefault() == null && countPredicate.CreatedAt.Date == crnt.AddDays(-1)))).Count(),
                             }).AsNoTracking().AsSplitQuery().ToListAsync();


        if (data is null) return Rr.NoData(data);
        return Rr.SuccessFetch(data);

    }

    public async Task<Responder<List<GetDailyAssignedLeads>>> GetDailyAssignedChassingLeadReportAsync()
    {
        var data = await _db.GetAllByAsync<User, GetDailyAssignedLeads>
        (
            DashboardMapper.DailyAssignChassingLeadsReport(), w => w.LeadAssignedsTo.Any(a => a.CreatedAt.Date == DateTime.UtcNow.Date)
            && w.usersRoles.Any(a => a.Fk_RoleId == 7)

        );
        if (data is null) return Rr.NoData(data);
        return Rr.SuccessFetch(data);
    }

    public async Task<Responder<List<GetDailyAssignedLeads>>> GetMonthlyAssignedChassingLeadReportAsync(MonthYearDto dto)
    {
        var date = dto.Month == null ? DateTime.UtcNow.Month : dto.Month;
        var dateYear = dto.Year == null ? DateTime.UtcNow.Year : dto.Year;
        var data = await _db.GetAllByAsync<User, GetDailyAssignedLeads>
        (
            DashboardMapper.MonthlyAssignChassingLeadsReport(dto), w => w.LeadAssignedsTo.Any(a => a.CreatedAt.Month == date && a.CreatedAt.Year == dateYear)
            && w.usersRoles.Any(a => a.Fk_RoleId == 7)
        );
        if (data is null) return Rr.NoData(data);
        return Rr.SuccessFetch(data);
    }

    //for agent daily report
    public async Task<Responder<DailyAgentsReportDto>> GetAgentDailyCountAsync(int agentId)
    {
        int? createdBy = agentId == 0 ? JwtHandlerRepo.GetCrntUserId(httpContext) : agentId;

        var data = await _db.GetByAsync<User, DailyAgentsReportDto>
        (
            w => w.ID == createdBy,
            DashboardMapper.DailyAgentStatusCount()
        );
        if (data is null) return Rr.NoData(data);
        return Rr.SuccessFetch(data);
    }

    public async Task<Responder<List<LeadFullDetailListDto>>> GetExcelReportAsync(int[] leadid)
    {
        if (leadid.Any())
            return Rr.SuccessFetch(await _db.GetAllByAsync<Lead, LeadFullDetailListDto>(DashboardMapper.GetExcelDetails, w => leadid.Contains(w.ID),
                orderBy: o => o.ID,
                IsAsending: false
                ));

        return Rr.SuccessFetch(await _db.GetAllByAsync<Lead, LeadFullDetailListDto>(DashboardMapper.GetExcelDetails));
    }

    public async Task<Responder<PdfDto>> GetPDFReportAsync(int leadid)
    {

        var data = await _db.GetByAsync<Lead, PdfDto>
        (
            w => w.ID == leadid,
            DashboardMapper.GetPdfDetails
        );
        if (data is null) return Rr.NoData(data);
        return Rr.SuccessFetch(data);
    }

    public async Task<Responder<DailyAgentsReportDto>> GetAgentMonthlyCountAsync(int agentId)
    {
        int? createdBy = agentId == 0 ? JwtHandlerRepo.GetCrntUserId(httpContext) : agentId;

        var data = await _db.GetByAsync<User, DailyAgentsReportDto>
        (
            w => w.ID == createdBy,
            DashboardMapper.MonthlyAgentStatusCount()
        );
        if (data is null) return Rr.NoData(data);
        return Rr.SuccessFetch(data);
    }


    public async Task<Responder<List<MonthDayAgentDto>>> GetAgentMonthlyDetailReportAsync(AgentIdDto dto)
    {

        int? createdBy = dto.AgentId == 0 ? JwtHandlerRepo.GetCrntUserId(httpContext) : dto.AgentId;

        var crnt = DateTime.UtcNow;
        var monthStart = new DateTime(dto.Year == null ? crnt.Year : dto.Year ?? 0, dto.Month == null ? crnt.Month : dto.Month ?? 0, 1);
        var endMonth = monthStart.AddMonths(1);
        int agentId = dto.AgentId ?? 0;
        var data = await _db.Db().Leads.Where(predicate => predicate.FK_CreatedBy == agentId
        && predicate.CreatedAt >= monthStart && predicate.CreatedAt <= endMonth
        )
            .GroupBy(keySelector => keySelector.CreatedAt.Date)
            .Select(selector => new MonthDayAgentDto()
            {
                AgentId = selector.FirstOrDefault().FK_CreatedBy ?? agentId,
                AgentName = selector.FirstOrDefault().CreatedBy.FirstName + " " + selector.FirstOrDefault().CreatedBy.LastName,
                Day = selector.FirstOrDefault().CreatedAt.Day,
                NewLead = selector.Count(countPredicate => countPredicate.Fk_StatusId == 1),
                EvError = selector.Count(countPredicate => countPredicate.Fk_StatusId == 4),
                QaPending = selector.Count(countPredicate => countPredicate.Fk_StatusId == 3),
                SnsFail = selector.Count(countPredicate => countPredicate.Fk_StatusId == 19),
                QaQualified = _db.Db().QAs.Include(l => l.Lead).Where(
                    predicate => predicate.Lead.FK_CreatedBy == agentId
                    && predicate.Lead.CreatedAt.Day == selector.FirstOrDefault().CreatedAt.Day && predicate.Fk_StatusId == 17)
                .Count(),
                NotQualified = _db.Db().QAs.Include(l => l.Lead).Where(
                    predicate => predicate.Lead.FK_CreatedBy == agentId
                    && predicate.Lead.CreatedAt.Day == selector.FirstOrDefault().CreatedAt.Day
                    && (predicate.Fk_StatusId == 16 || predicate.Fk_StatusId == 27)
                    ).Count(),
            }).AsNoTracking().AsSplitQuery().ToListAsync();


        //var check = await _db.Db().Users.Where(predicate => predicate.ID == agentId
        //&& predicate.Lead.Any(anyPredicate => anyPredicate.CreatedAt >= startMonth && anyPredicate.CreatedAt <= endMonth)
        //).ToListAsync();

        if (data is null) return Rr.NoData(data);
        return Rr.SuccessFetch(data);
    }

}//repo class

