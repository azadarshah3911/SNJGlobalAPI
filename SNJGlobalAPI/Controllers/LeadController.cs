using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{
    [Route("Production/lead")]
    [ApiController]
    [Authorize]
    public class LeadController : ControllerBase
    {
        private readonly ILead _repo;
        public LeadController(ILead repo) => _repo = repo;

        [HttpPost("Post")]
        public async Task<IActionResult> Post(AddLeadDto dto) => Ok(await _repo.AddLeadAsync(dto));

        [HttpGet("Get")]
        [Authorize(Roles = $"{appRolesNameDto.ChassingManager},{appRolesNameDto.QaManager},{appRolesNameDto.SuperAdmin},{appRolesNameDto.TeamLead},{appRolesNameDto.ProductionManager}")]
        public async Task<IActionResult> Get(int leadId) => Ok(await _repo.GetLeadByIdAsync(leadId));

        [HttpGet("GetAll")]
        [Authorize(Roles = $"{appRolesNameDto.ChassingManager},{appRolesNameDto.QaManager},{appRolesNameDto.SuperAdmin},{appRolesNameDto.TeamLead},{appRolesNameDto.ProductionManager},{appRolesNameDto.Agent}")]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllLeadAsync());

        [HttpGet("GetAllRejected")]
        public async Task<IActionResult> GetAllRejected() => Ok(await _repo.GetAllRejectedAsync());

        [HttpPost("DeleteLead")]
        [Authorize(Roles = $"{appRolesNameDto.ChassingManager},{appRolesNameDto.QaManager},{appRolesNameDto.SuperAdmin},{appRolesNameDto.ProductionManager}")]
        public async Task<IActionResult> DeleteLead(int leadId) => Ok(await _repo.DeleteLeadAsync(leadId));

    }
}
