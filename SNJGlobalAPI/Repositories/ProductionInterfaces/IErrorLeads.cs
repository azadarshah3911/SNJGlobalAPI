using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.ProductionInterfaces
{
    public interface IErrorLeads
    {
        Task<Responder<List<GetErrorLeadListDto>>> GetAllErrorLeads();
    }

    
}
