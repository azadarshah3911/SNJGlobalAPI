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
    public class AgentBonusRepo : IAgentBonus
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public AgentBonusRepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }

        public async Task<Responder<object>> AddAgentBonusAsync(AddAgentBonusDto dto)
        {
            var map = _mapper.Map<UserBonus>(dto);
            var user = JwtHandlerRepo.GetCrntUserId(httpContext);
            map.Fk_BonusFrom = user;
            var bonus = await _db.GetAsync<UserBonus>(w => w.Fk_BonusTo == dto.Fk_BonusTo && w.CreatedAt.Date == DateTime.Now.Date);
            
            if(bonus is not null)
            {
                bonus.Fk_BonusFrom = user;
                bonus.Amount = dto.Amount;
                if (!await _db.UpdateAsync(bonus))
                    return Rr.Fail<object>("Update");
            }
            else
                if (!await _db.PostAsync(map))
                    return Rr.Fail<object>("Create");
           
            return Rr.Success<object>("Created", map.ID);
        }

        public async Task<Responder<List<GetAgentBonusDto>>> GetAllBonusByAgentIdAsync(int agentId)
        {
            var data = new List<GetAgentBonusDto>();
            if (agentId > 0)
                data = await _db.GetAllByAsync<UserBonus, GetAgentBonusDto>(AgentHistoryMapper.GetBonus,
                  w => w.Fk_BonusTo == agentId
               );
            else
                data = await _db.GetAllByAsync<UserBonus, GetAgentBonusDto>(AgentHistoryMapper.GetBonus);
            return Rr.SuccessFetch(data);
        }
    }
}
