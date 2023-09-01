using AutoMapper;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DbModelsProduction;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.GeneralServices;
using SNJGlobalAPI.Mappers;
using SNJGlobalAPI.Repositories.CommonInterfaces;

namespace SNJGlobalAPI.Repositories.CommonRepos
{
    public class DropDownRepo : IDropDown
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        public DropDownRepo(IDb db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Responder<List<GetStateDdDto>>> StateDdAsync() => 
            Rr.SuccessFetch(
           await _db.GetAllByAsync<State, GetStateDdDto>(
                       Map.StateDd()));
        public async Task<Responder<List<GetBranchDdDto>>> BranchDdAsync() =>
           Rr.SuccessFetch(
          await _db.GetAllByAsync<Branch, GetBranchDdDto>(
                      Map.BranchDd()));

        public async Task<Responder<List<GetStatusDdDto>>> StatusesDdAsync(int stageId) =>
          Rr.SuccessFetch(
         await _db.GetAllByAsync<Status, GetStatusDdDto>(predicate: p => p.Fk_StageId == stageId));

        public async Task<Responder<List<GetAgentDdDto>>> AgentDdAsync() =>
          Rr.SuccessFetch(
         await _db.GetAllByAsync<User, GetAgentDdDto>(Map.AgenthDd()));
    }
}
