using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.ProductionInterfaces
{
    public interface IQA
    {
        Task<Responder<object>> AddQAAsync(AddQaDto dto);
        Task<Responder<List<GetQAListDto>>> GetAllAsync(SearchDto dto);
        Task<Responder<List<GetQAListDto>>> GetAllNotQualifiedAsync(SearchDto dto);
        Task<Responder<GetQaDetailDto>> GetByLeadIdAsync(int leadId);
        Task<Responder<List<GetQAListForAgentDto>>> GetAllForAgentAsync(SearchDto dto);
        Task<Responder<List<GetQAListDto>>> GetAllOtherPlansAsync(SearchDto dto);
    }
}
