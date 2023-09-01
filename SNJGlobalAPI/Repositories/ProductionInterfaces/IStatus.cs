using SNJGlobalAPI.DtoModels;

namespace SNJGlobalAPI.Repositories.ProductionInterfaces
{
    public interface IStatus
    {
        Task<Responder<object>> AddLeadStatusAsync(int leadID, int statusId, int userId);
        Task<Responder<object>> RevertLeadStatusAsync(int leadID);
    }
}
