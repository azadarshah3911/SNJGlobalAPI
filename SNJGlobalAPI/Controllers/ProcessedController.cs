using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{

    [Route("Production/processed")]
    [ApiController]
    [Authorize(Roles = $"{appRolesNameDto.ChassingManager},{appRolesNameDto.ChassingVerificationAgent},{appRolesNameDto.ChassingAgent},{appRolesNameDto.SuperAdmin}")]
    public class ProcessedController : ControllerBase  
    {
        private readonly IProcessed _repo;
        public ProcessedController(IProcessed repo) => _repo = repo;

        [HttpPost("GetCgm")]
        public async Task<IActionResult> GetCgm(SearchDto dto) => Ok(await _repo.GetAllCgmLeadAsync(dto));


        [HttpPost("GetBraces")]
        public async Task<IActionResult> GetBraces(SearchDto dto) => Ok(await _repo.GetAllBracesLeadAsync(dto));


        [HttpPost("GetCgmForAgent")]
        public async Task<IActionResult> GetCgmForAgent() => Ok(await _repo.GetCgmForAgent());


        [HttpPost("GetBracesForAgent")]
        public async Task<IActionResult> GetBracesForAgent() => Ok(await _repo.GetBracesForAgent());


        [HttpPost("Post")]
        public async Task<IActionResult> Post([FromForm]AddChassingDto dto) => Ok(await _repo.AddChassinAsync(dto));

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int leadId) => Ok(await _repo.GetByLeadIdAsync(leadId));

        [HttpGet("GetDeniedLeads")]
        public async Task<IActionResult> GetDeniedLeads() => Ok(await _repo.GetDeniedLeadsAsync());

        [HttpGet("GetDeniedLeadsForAgent")]
        public async Task<IActionResult> GetDeniedLeadsForAgent() => Ok(await _repo.GetDeniedForAgentAsync());

    }
}
