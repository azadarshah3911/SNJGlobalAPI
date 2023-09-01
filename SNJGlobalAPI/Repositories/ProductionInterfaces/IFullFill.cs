using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.ProductionInterfaces
{
    public interface IFullFill
    {
        Task<Responder<List<leadListDto>>> GetAllAsync();
    }
}
