using AutoMapper;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DbModelsProduction;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.GeneralServices;
using SNJGlobalAPI.Repositories.CommonInterfaces;
using SNJGlobalAPI.Repositories.ProductionInterfaces;

namespace SNJGlobalAPI.Repositories.ProductionRepos
{
    public class ProductQuestionRepo : IProductQuestion
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public ProductQuestionRepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }


        public async Task<Responder<List<GetProductQuestionForAgentDto>>> GetAllProductQuestionForAgentAsync(GetProductsAndSendQuestionsDto productId)
        {
            var ins = new List<GetProductQuestionForAgentDto>();
            foreach (var item in productId.ProductsId)
            {
                ins.AddRange( 
                    await _db.GetAllByAsync<ProductQuestion, GetProductQuestionForAgentDto>(predicate: w => w.FK_ProductId == item && w.IsActive && w.FK_StageId == 1)
                    );
            }
            if (!ins.Any())
                return Rr.NoData(ins);
            return Rr.SuccessFetch(ins);
        }

        public async Task<Responder<List<GetProductQuestionForAgentDto>>> GetAllProductQuestionForStage4Async(GetProductsAndSendQuestionsDto productId)
        {
            var ins = new List<GetProductQuestionForAgentDto>();
            foreach (var item in productId.ProductsId)
            {
                ins.AddRange(
                    await _db.GetAllByAsync<ProductQuestion, GetProductQuestionForAgentDto>(predicate: w => w.FK_ProductId == item && w.IsActive && w.FK_StageId == 4)
                    );
            }
            if (!ins.Any())
                return Rr.NoData(ins);
            return Rr.SuccessFetch(ins);
        }
        public async Task<Responder<string>> EditProductQuestionAnswers(List<EditProductQuestionAnswerDto> dto)
        {
            var tans = await _db.BeginTranAsync();
            var id = dto.Select(selector => selector.Id).ToList();
            var data = await _db.GetAllAsync<ProductQuestionAnswer>(predicate => id.Contains(predicate.ID));
            foreach (var item in dto)
            {
                var edit = data.Where(predicate => predicate.ID == item.Id).FirstOrDefault();
                edit.Answer = item.Answer;
                
                if(!await _db.UpdateAsync(edit))
                {
                    await tans.RollbackAsync();
                    return Rr.Fail<string>("updated");
                }

            }

            await tans.CommitAsync();
            return Rr.Success<string>("updated");
        }
    }
}
