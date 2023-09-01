using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.ProductionInterfaces
{
    public interface IChassingVerification
    {
        Task<Responder<object>> AddChassingVerificationAsync(AddChassingVerificationDto dto);
        Task<Responder<List<GetQAListDto>>> GetAllLeadAsync(SearchDto dto);
    }
}
