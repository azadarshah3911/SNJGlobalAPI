using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.ProductionInterfaces
{
    public interface IConfirmation
    {
        Task<Responder<object>> AddAsync(AddConfiramtionDto dto);
        Task<Responder<List<leadListDto>>> GetAllAsync();
    }
}
