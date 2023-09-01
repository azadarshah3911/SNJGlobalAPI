using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.Repositories.Interfaces;

namespace SNJGlobalAPI.Controllers
{
    [Route("Production/product")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _repo;
        public ProductController(IProduct repo) => _repo = repo;

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id) => Ok(await _repo.GetProductAsync(id));

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(SearchDto search) =>
            Ok(await _repo.GetAllProductAsync(search));

        [HttpPost("Post")]
        public async Task<IActionResult> Post(AddProductDto dto) => Ok(await _repo.AddProductAsync(dto));

        [HttpPut("Update")]
        public async Task<IActionResult> Update(ProductUpdateDto dto) => Ok(await _repo.UpdateProductAsync(dto));

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id) => Ok(await _repo.DeleteProductAsync(id));

        [HttpGet("GetAllSubProductForAgent")]
        public async Task<IActionResult> GetAllSubProductForAgent() =>
          Ok(await _repo.GetAllSubProductForAgentAsync());

        [HttpGet("GetAllSubProductForSns")]
        public async Task<IActionResult> GetAllSubProductForSns(int leadId) =>
         Ok(await _repo.GetAllSubProductForSnsAsync(leadId));
    }
}
