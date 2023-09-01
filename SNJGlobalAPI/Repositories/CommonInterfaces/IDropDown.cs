using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.CommonInterfaces
{
    public interface IDropDown
    {
        Task<Responder<List<GetStateDdDto>>> StateDdAsync();
        Task<Responder<List<GetBranchDdDto>>> BranchDdAsync();
        Task<Responder<List<GetStatusDdDto>>> StatusesDdAsync(int stageId);
        Task<Responder<List<GetAgentDdDto>>> AgentDdAsync();
    }
}
