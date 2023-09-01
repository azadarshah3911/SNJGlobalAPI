using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.ProductionInterfaces
{
    public interface IAgentBonus
    {
        Task<Responder<object>> AddAgentBonusAsync(AddAgentBonusDto dto);
        Task<Responder<List<GetAgentBonusDto>>> GetAllBonusByAgentIdAsync(int agentId);

    }
}
