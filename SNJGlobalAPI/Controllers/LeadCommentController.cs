
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.Repositories.CommonRepos;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{
    [Route("Production/leadcomment")]
    [ApiController]
    [Authorize]

    public class LeadCommentController : ControllerBase
    {
        private readonly ILeadComment _repo;
        public LeadCommentController(ILeadComment repo) => _repo = repo;

        [HttpPost("Post")]
        public async Task<IActionResult> Post(AddLeadCommentDto dto) => Ok(await _repo.AddLeadCommentAsync(dto));

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int leadId) => Ok(await _repo.GetCommentByLeadId(leadId));
    }
}
