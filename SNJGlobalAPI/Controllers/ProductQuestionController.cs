using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.Repositories.CommonRepos;
using SNJGlobalAPI.Repositories.Interfaces;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Controllers
{
    [Route("Production/productquestion")]
    [ApiController]
    [Authorize]

    public class ProductQuestionController : ControllerBase
    {
        private readonly IProductQuestion _repo;
        public ProductQuestionController(IProductQuestion repo) => _repo = repo;

        [HttpPost("GetAllForAgent")]
        public async Task<IActionResult> GetAllForAgent(GetProductsAndSendQuestionsDto productId) =>
         Ok(await _repo.GetAllProductQuestionForAgentAsync(productId));

        [HttpPost("GetAllForQa")]
        public async Task<IActionResult> GetAllForQa(GetProductsAndSendQuestionsDto productId) =>
       Ok(await _repo.GetAllProductQuestionForStage4Async(productId));

        [HttpPost("UpdateQuestionAnswer")]
        public async Task<IActionResult> UpdateQuestionAnswer(List<EditProductQuestionAnswerDto> dto) =>
       Ok(await _repo.EditProductQuestionAnswers(dto));

    }
}
