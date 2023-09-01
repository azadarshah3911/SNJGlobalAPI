using AutoMapper;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.GeneralServices;
using SNJGlobalAPI.Mappers;
using SNJGlobalAPI.Repositories.CommonInterfaces;
using SNJGlobalAPI.Repositories.ProductionInterfaces;
using SNJGlobalAPI.SecurityHandlers;

namespace SNJGlobalAPI.Repositories.ProductionRepos
{
    public class LeadCommentRepo : ILeadComment
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public LeadCommentRepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }


        public async Task<Responder<object>> AddLeadCommentAsync(AddLeadCommentDto dto)
        {
            if (!await _db.IsAnyAsync<Lead>(w => w.ID == dto.FK_LeadID))
                return Rr.NotFound<object>("Lead", dto.FK_LeadID.ToString());

            var map = _mapper.Map<LeadComments>(dto);
            map.FK_CreatedBy = JwtHandlerRepo.GetCrntUserId(httpContext);

            if (!await _db.PostAsync(map)) return Rr.Fail<object>("Create");

            return Rr.Success<object>("Created", map.ID);
        }

        public async Task<Responder<List<GetLeadCommentDto>>> GetCommentByLeadId(int leadId)
        {
            var data = await _db.GetAllByAsync<LeadComments, GetLeadCommentDto>(LeadCommentMapper.GetLeadComments, w =>
            w.FK_LeadID == leadId);
            return Rr.SuccessFetch(data);
        }
    }
}
