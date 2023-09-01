using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.ProductionInterfaces
{
    public interface IPatient
    {
        Task<Responder<object>> AddPatientAsync(AddPatientDto dto);
        Task<Responder<GetPatientDto>> GetPatientAsync(int id);
        Task<Responder<List<GetPatientDto>>> GetAllPatientAsync(SearchDto search);
        Task<Responder<object>> DeletePatientAsync(int id);
        Task<Responder<string>> UpdatePatientAsync(UpdatePatientDto dto ,int stageId, int leadId);
        Task<Responder<GetPatientFoAgentDto>> GetPatientByMedicarIdAsync(string medicareid);
        Task<Responder<List<PatientLeadListDto>>> GetPatientLeadCountAsync();
        Task<Responder<GetPatientForAgenBySsntDto>> GetPatientBySsnAsync(string ssn);
        Task<Responder<GetEditPatientForAgentDto>> GetEditPatientAsync(int leadId);
    }
}
