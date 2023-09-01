using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.ProductionInterfaces
{
    public interface ISns
    {
        Task<Responder<object>> AddSnsAsync(AddSnsDto dto);
        Task<Responder<object>> UpdateSnsAsync(UpdateSnsDto dto);
        Task<Responder<List<GetSnsPendingAndByPassListDto>>> GetAllSnsAsync();
        Task<Responder<List<GetSnsPendingAndByPassListDto>>> GetAllSnsFailAsync();
        Task<Responder<GetSnsDetailDto>> GetSnsByLeadIdAsync(int leadId);
    }
}
