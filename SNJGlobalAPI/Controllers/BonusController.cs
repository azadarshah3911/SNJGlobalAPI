using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{

    [Route("Production/bonus")]
    [ApiController]
    [Authorize]

    public class BonusController : ControllerBase
    {
        private readonly IAgentBonus _repo;
        public BonusController(IAgentBonus repo) => _repo = repo;

        [HttpPost("Post")]
        public async Task<IActionResult> Post(AddAgentBonusDto dto) => Ok(await _repo.AddAgentBonusAsync(dto));


        [HttpGet("Get")]
        public async Task<IActionResult> Get(int agentId) => Ok(await _repo.GetAllBonusByAgentIdAsync(agentId));
    }
}
