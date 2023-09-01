using AutoMapper;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DbModelsProduction;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.GeneralServices;
using SNJGlobalAPI.Mappers;
using SNJGlobalAPI.Repositories.CommonInterfaces;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Repositories.ProductionRepos
{
    public class AgentHistoryRepo : IAgentHistory
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public AgentHistoryRepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }
        public async Task<Responder<List<GetAgentPenaltyDto>>> GetAllPenaltyByAgentIdAsync(int agentId)
        {
            var data = new List<GetAgentPenaltyDto>();
            if (agentId > 0)
                data = await _db.GetAllByAsync<AgentPenalty, GetAgentPenaltyDto>(AgentHistoryMapper.GetPenalty,
                  w => w.Fk_PenaltyTo == agentId
               );
            else
             data = await _db.GetAllByAsync<AgentPenalty, GetAgentPenaltyDto>(AgentHistoryMapper.GetPenalty);
            return Rr.SuccessFetch(data);
        }
    }
}
