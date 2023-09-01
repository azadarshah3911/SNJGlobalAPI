using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{
    [Route("Production/agenthistory")]
    [ApiController]
    [Authorize]
    public class AgentHistoryController : ControllerBase
    {
        private readonly IAgentHistory _repo;
        public AgentHistoryController(IAgentHistory repo) => _repo = repo;

        [HttpGet("GetAgentPenalty")]
        public async Task<IActionResult> GetAgentPenalty(int agentId) => Ok(await _repo.GetAllPenaltyByAgentIdAsync(agentId));
    }
}
