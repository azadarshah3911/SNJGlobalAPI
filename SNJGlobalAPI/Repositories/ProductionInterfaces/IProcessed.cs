using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.ProductionInterfaces
{
    public interface IProcessed
    {
        Task<Responder<List<GetQAListDto>>> GetAllCgmLeadAsync(SearchDto dto);
        Task<Responder<List<GetQAListDto>>> GetAllBracesLeadAsync(SearchDto dto);
        Task<Responder<List<GetQAListForAgentDto>>> GetBracesForAgent();
        Task<Responder<List<GetQAListForAgentDto>>> GetCgmForAgent();
        Task<Responder<object>> AddChassinAsync(AddChassingDto dto);
        Task<Responder<GetProcessedDetailsDto>> GetByLeadIdAsync(int leadId);
        Task<Responder<List<GetQAListDto>>> GetDeniedLeadsAsync();
        Task<Responder<List<GetQAListForAgentDto>>> GetDeniedForAgentAsync();
    }
}
