using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{

    [Route("Production/confirmation")]
    [ApiController]
    [Authorize(Roles = $"{appRolesNameDto.ChassingManager},{appRolesNameDto.ChassingAgent},{appRolesNameDto.SuperAdmin}")]
    public class ConfirmationController : ControllerBase
    {
        private readonly IConfirmation _repo;
        public ConfirmationController(IConfirmation repo) => _repo = repo;

        [HttpPost("Post")]
        public async Task<IActionResult> Post(AddConfiramtionDto dto) => Ok(await _repo.AddAsync(dto));


        [HttpGet("Get")]
        public async Task<IActionResult> Get() => Ok(await _repo.GetAllAsync());

    }
}
