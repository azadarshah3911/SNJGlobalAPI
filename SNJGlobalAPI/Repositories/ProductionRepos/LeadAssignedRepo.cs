using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DbModelsProduction;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.GeneralServices;
using SNJGlobalAPI.Mappers;
using SNJGlobalAPI.Repositories.CommonInterfaces;
using SNJGlobalAPI.Repositories.ProductionInterfaces;
using SNJGlobalAPI.SecurityHandlers;
using System.Linq;

namespace SNJGlobalAPI.Repositories.ProductionRepos
{
    public class LeadAssignedRepo : ILeadAssigned
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public LeadAssignedRepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }
        public async Task<Responder<object>> AddLeadAssignedAsync(AddLeadAssignedDto dto , int stageId)
        {
            int? createdBy = JwtHandlerRepo.GetCrntUserId(httpContext);
            var getassinedLeads = await _db.Db().LeadAssigneds.Select(selector => new GetLeadAssignedDto
            {
                FK_LeadId = selector.FK_LeadId,
                FK_StageId = selector.FK_StageId,
            }).ToListAsync();
            
            List<LeadAssigned> leadAssigned = new(); 
            foreach (var item in dto.FK_LeadId)
            {
                if(!getassinedLeads.Exists(where => where.FK_LeadId == item && where.FK_StageId == stageId))
                    leadAssigned.Add(new()
                    {
                        FK_LeadId = item,
                        FK_AgentId = dto.FK_AgentId,
                        FK_SupervisorId = createdBy,
                        FK_StageId = stageId
                    });
            }

            if (!await _db.PostRangeAsync(leadAssigned)) return Rr.Fail<object>("Create");

            return Rr.Success<object>("Created");
        }
    }
}
