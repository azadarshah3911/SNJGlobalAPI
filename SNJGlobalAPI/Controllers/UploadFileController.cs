using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.Repositories.CommonInterfaces;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{
    [Route("Production/uploadfile")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly IUploadFile _repo;
        public UploadFileController(IUploadFile repo) => _repo = repo;

        [HttpPost("Post")]
        public async Task<IActionResult> Post([FromForm] UploadFileDto dto) => Ok(await _repo.UploadFile(dto,"Test"));
    }
}
