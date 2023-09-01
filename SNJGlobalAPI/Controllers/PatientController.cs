using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{
    [Route("Production/patient")]
    [ApiController]
    [Authorize]

    public class PatientController : ControllerBase
    {
        private readonly IPatient _repo;
        public PatientController(IPatient repo) => _repo = repo;

         [HttpPost("Post")]
        public async Task<IActionResult> Post(AddPatientDto dto) => Ok(await _repo.AddPatientAsync(dto));

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id) => Ok(await _repo.GetPatientAsync(id));


        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(SearchDto search) =>
         Ok(await _repo.GetAllPatientAsync(search));


        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdatePatientDto dto, int stageId, int leadId) => Ok(await _repo.UpdatePatientAsync(dto,stageId,leadId));

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id) => Ok(await _repo.DeletePatientAsync(id));

        [HttpGet("GetForAgent")]
        public async Task<IActionResult> GetForAgent(string medicareid) => Ok(await _repo.GetPatientByMedicarIdAsync(medicareid));

        [HttpGet("GetForAgentBySsn")]
        public async Task<IActionResult> GetForAgentBySsn(string ssn) => Ok(await _repo.GetPatientBySsnAsync(ssn));

        [HttpGet("GetAllPatientWithLeadCount")]
        public async Task<IActionResult> GetAllPatientWithLeadCount() => Ok(await _repo.GetPatientLeadCountAsync());


        [HttpGet("GetEditLead")]
        public async Task<IActionResult> GetEditLead(int leadId) => Ok(await _repo.GetEditPatientAsync(leadId));

    }
}
