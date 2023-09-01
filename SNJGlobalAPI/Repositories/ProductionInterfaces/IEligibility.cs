using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.ProductionInterfaces
{
    public interface IEligibility
    {
        Task<Responder<object>> AddEligibilityAsync(AddEligibilityDto dto);
        Task<Responder<object>> EditEligibilityAsync(EditEligibilityDto dto);
        Task<Responder<List<GetNewLeadListDto>>> GetAllNewLeadsAsync(SearchDto search);
        Task<Responder<GetNewLeadDetailsDto>> GetByLeadIdAsync(int leadid);
        Task<Responder<List<GetNewLeadListDto>>> GetAllEvErrorsAsync(SearchDto search);
    }
}
