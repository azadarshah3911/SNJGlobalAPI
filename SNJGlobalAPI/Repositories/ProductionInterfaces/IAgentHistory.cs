using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.ProductionInterfaces
{
    public interface IAgentHistory
    {
        Task<Responder<List<GetAgentPenaltyDto>>> GetAllPenaltyByAgentIdAsync(int agentId);
    }
}
