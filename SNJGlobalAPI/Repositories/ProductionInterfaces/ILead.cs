using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.ProductionInterfaces
{
    public interface ILead
    {
        Task<Responder<object>> AddLeadAsync(AddLeadDto dto);
        Task<Responder<GetLeadDetailsDto>> GetLeadByIdAsync(int id);
        Task<Responder<List<leadListDto>>> GetAllLeadAsync();
        Task<Responder<List<leadListDto>>> GetAllRejectedAsync();
        Task<Responder<object>> DeleteLeadAsync(int leadId);
        /*  Task<Responder<GetProductDto>> GetLeadAsync(int id);
          Task<Responder<List<GetProductDto>>> GetAllLeadAsync(SearchDto search);
          Task<Responder<object>> DeleteLeadAsync(int id);
          Task<Responder<object>> UpdateLeadAsync(ProductUpdateDto dto);*/
    }
}
