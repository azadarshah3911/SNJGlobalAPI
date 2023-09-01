using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{
    [Route("Production/leadassign")]
    [ApiController]
    [Authorize(Roles = $"{appRolesNameDto.ChassingManager},{appRolesNameDto.QaManager},{appRolesNameDto.SuperAdmin}")]

    public class LeadAssignController : ControllerBase
    {
        private readonly ILeadAssigned _repo;
        public LeadAssignController(ILeadAssigned repo) => _repo = repo;


        [HttpPost("QaPost")]
        public async Task<IActionResult> QaPost(AddLeadAssignedDto dto) => Ok(await _repo.AddLeadAssignedAsync(dto , 4));

        [HttpPost("ProcessPost")]
        public async Task<IActionResult> ProcessPost(AddLeadAssignedDto dto) => Ok(await _repo.AddLeadAssignedAsync(dto, 5));

    }
}
