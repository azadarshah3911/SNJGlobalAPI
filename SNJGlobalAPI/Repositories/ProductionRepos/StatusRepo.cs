using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.GeneralServices;
using SNJGlobalAPI.Repositories.CommonInterfaces;
using SNJGlobalAPI.Repositories.ProductionInterfaces;
using SNJGlobalAPI.SecurityHandlers;

namespace SNJGlobalAPI.Repositories.ProductionRepos
{
    public class StatusRepo : IStatus
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public StatusRepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }

        public async Task<Responder<object>> AddLeadStatusAsync(int leadID, int statusId, int userId)
        {
            var data = await _db.GetAsync<Lead>(w => w.ID == leadID);
            if (data is not null)
                return Rr.NotFound<object>("Lead", leadID.ToString());

            if (!await _db.PostAsync<LeadStatus>(new()
            {
                FK_StatusId = statusId,
                FK_LeadId = leadID,
                FK_CreatedBy = userId
            }))
                return Rr.Fail<object>("update");

            return Rr.Success<object>("Add");
        }

        public async Task<Responder<object>> RevertLeadStatusAsync(int leadId)
        {
            var lead = await _db.GetAsync<Lead>(w => w.ID == leadId);
            if (lead is null)
                return Rr.NotFound<object>("Lead", leadId.ToString());

            if(lead.Fk_StatusId == 1)
                return Rr.Custom<object>("can't changed status", leadId.ToString());

            var statusId = await _db.Db().LeadStatuses.Where(w => w.FK_LeadId == leadId).OrderByDescending(o => o.ID).Skip(1).Select(s => s.FK_StatusId).FirstOrDefaultAsync();

            int? createdBy = JwtHandlerRepo.GetCrntUserId(httpContext);

            if (!await _db.PostAsync<LeadStatus>(new()
            {
                FK_StatusId = statusId,
                FK_LeadId = leadId,
                FK_CreatedBy = createdBy
            }))
                return Rr.Fail<object>("update");

            return Rr.Success<object>("update");
        }
    }
}
