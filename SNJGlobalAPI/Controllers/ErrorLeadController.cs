using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{

    [Route("Production/errorlead")]
    [ApiController]
    [Authorize]
    public class ErrorLeadController : ControllerBase
    {
        private readonly IErrorLeads _repo;
        public ErrorLeadController(IErrorLeads repo) => _repo = repo;

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllErrorLeads());

    }
}
