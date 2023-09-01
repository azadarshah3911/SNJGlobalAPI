using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{

    [Route("Production/fullfill")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Chassing Manager")]
    public class FullFillController : ControllerBase
    {
        private readonly IFullFill _repo;
        public FullFillController(IFullFill repo) => _repo = repo;


        [HttpGet("Get")]
        public async Task<IActionResult> Get() => Ok(await _repo.GetAllAsync());

    }
}
