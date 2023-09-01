using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.ProductionInterfaces
{
    public interface ILeadComment
    {
        Task<Responder<object>> AddLeadCommentAsync(AddLeadCommentDto dto);
        Task<Responder<List<GetLeadCommentDto>>> GetCommentByLeadId(int leadId);
    }
}
