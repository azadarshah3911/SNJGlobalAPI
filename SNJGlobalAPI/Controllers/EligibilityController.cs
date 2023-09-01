using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{
    [Route("Production/eligibility")]
    [ApiController]
    [Authorize(Roles = $"{appRolesNameDto.QaAgent},{appRolesNameDto.LeadFixer},{appRolesNameDto.ProductionManager},{appRolesNameDto.QaManager},{appRolesNameDto.SuperAdmin}")]

    public class EligibilityController : ControllerBase
    {
        private readonly IEligibility _repo;
        public EligibilityController(IEligibility repo) => _repo = repo;

        [HttpPost("Get")]
        public async Task<IActionResult> Get(SearchDto dto) => Ok(await _repo.GetAllNewLeadsAsync(dto));

        [HttpGet("GetByLeadId")]
        public async Task<IActionResult> GetByLeadId(int leadid) => Ok(await _repo.GetByLeadIdAsync(leadid));


        [HttpPost("Post")]
        public async Task<IActionResult> Post([FromForm]AddEligibilityDto dto) => Ok(await _repo.AddEligibilityAsync(dto));

        [HttpPost("GetEvrror")]
        public async Task<IActionResult> GetEvrror(SearchDto dto) => Ok(await _repo.GetAllEvErrorsAsync(dto));

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(EditEligibilityDto dto) => Ok(await _repo.EditEligibilityAsync(dto));


    }
}
