using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{
    [Route("Production/sns")]
    [ApiController]
    [Authorize(Roles = $"{appRolesNameDto.QaManager},{appRolesNameDto.QaAgent},{appRolesNameDto.SuperAdmin},{appRolesNameDto.ChassingManager},{appRolesNameDto.ChassingAgent}")]
    public class SnsController : ControllerBase
    {
        private readonly ISns _repo;
        public SnsController(ISns repo) => _repo = repo;

        [HttpPost("Post")]
        public async Task<IActionResult> Post(AddSnsDto dto) => Ok(await _repo.AddSnsAsync(dto));

        [HttpGet("Get")]
        public async Task<IActionResult> Get() => Ok(await _repo.GetAllSnsAsync());

        [HttpGet("Get/{leadId}")]
        public async Task<IActionResult> Get(int leadId) => Ok(await _repo.GetSnsByLeadIdAsync(leadId));

        [HttpGet("GetAllFail")]
        public async Task<IActionResult> GetAllFail() => Ok(await _repo.GetAllSnsFailAsync());

    }
}
