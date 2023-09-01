using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{
    [Route("Production/chassingverifcation")]
    [ApiController]
    [Authorize(Roles = $"{appRolesNameDto.ChassingManager},{appRolesNameDto.ChassingVerificationAgent},{appRolesNameDto.SuperAdmin}")]
    public class ChassingVerificationController : ControllerBase
    {
        private readonly IChassingVerification _repo;
        public ChassingVerificationController(IChassingVerification repo) => _repo = repo;

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(SearchDto dto) => Ok(await _repo.GetAllLeadAsync(dto));

        [HttpPost("Post")]
        public async Task<IActionResult> Post(AddChassingVerificationDto dto) => Ok(await _repo.AddChassingVerificationAsync(dto));

    }
}
