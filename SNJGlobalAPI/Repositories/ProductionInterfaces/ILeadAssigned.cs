using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.ProductionInterfaces
{
    public interface ILeadAssigned
    {
        Task<Responder<object>> AddLeadAssignedAsync(AddLeadAssignedDto dto , int stageId);
    }
}
