using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{
    [Route("Production/leadstatus")]
    [ApiController]
    [Authorize]
    public class LeadStatusController : ControllerBase
    {
        private readonly IStatus _repo;
        public LeadStatusController(IStatus repo) => _repo = repo;

        [HttpGet("Get")]
        [Authorize(Roles = $"{appRolesNameDto.ChassingManager},{appRolesNameDto.QaManager},{appRolesNameDto.SuperAdmin},{appRolesNameDto.TeamLead},{appRolesNameDto.ProductionManager}")]
        public async Task<IActionResult> Get(int leadId) => Ok(await _repo.RevertLeadStatusAsync(leadId));

    }
}
