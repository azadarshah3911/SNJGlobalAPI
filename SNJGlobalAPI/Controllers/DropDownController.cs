using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.Repositories.CommonInterfaces;

namespace SNJGlobalAPI.Controllers
{
    [Route("Production/dropdown")]
    [ApiController]

    [Authorize]
    public class DropDownController : ControllerBase
    {
        private readonly IDropDown _repo;
        public DropDownController(IDropDown repo) => _repo = repo;

        [HttpGet("GetState")]
        public async Task<IActionResult> GetState() => Ok(await _repo.StateDdAsync());

        [HttpGet("GetBranch")]
        public async Task<IActionResult> GetBranch() => Ok(await _repo.BranchDdAsync());


        [HttpGet("GetStatus")]
        public async Task<IActionResult> GetStatus(int stageid) => Ok(await _repo.StatusesDdAsync(stageid));

        [HttpGet("GetAgent")]
        public async Task<IActionResult> GetAgent() => Ok(await _repo.AgentDdAsync());


    }
}
