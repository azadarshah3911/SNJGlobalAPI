using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{
    [Route("Production/qa")]
    [ApiController]
    [Authorize(Roles = $"{appRolesNameDto.QaManager},{appRolesNameDto.QaAgent},{appRolesNameDto.SuperAdmin}")]
    public class QaController : ControllerBase
    {
        private readonly IQA _repo;
        public QaController(IQA repo) => _repo = repo;

        [HttpPost("Post")]
        public async Task<IActionResult> Post([FromForm]AddQaDto dto) => Ok(await _repo.AddQAAsync(dto));

        [HttpPost("Get")]
        public async Task<IActionResult> Get(SearchDto dto) => Ok(await _repo.GetAllAsync(dto));

        [HttpGet("Get/{leadId}")]
        public async Task<IActionResult> Get(int leadId) => Ok(await _repo.GetByLeadIdAsync(leadId));

        [HttpPost("GetForAgent")]
        public async Task<IActionResult> GetForAgent(SearchDto dto) => Ok(await _repo.GetAllForAgentAsync(dto));

        [HttpPost("GetNotQualified")]
        public async Task<IActionResult> GetNotQualified(SearchDto dto) => Ok(await _repo.GetAllNotQualifiedAsync(dto));

        [HttpPost("GetOtherPlans")]
        public async Task<IActionResult> GetOtherPlans(SearchDto dto) => Ok(await _repo.GetAllOtherPlansAsync(dto));


    }
}
