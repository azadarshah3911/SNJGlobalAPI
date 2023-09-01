using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.ProductionInterfaces
{
    public interface IProductQuestion
    {
        Task<Responder<List<GetProductQuestionForAgentDto>>> GetAllProductQuestionForAgentAsync(GetProductsAndSendQuestionsDto productId);
        Task<Responder<List<GetProductQuestionForAgentDto>>> GetAllProductQuestionForStage4Async(GetProductsAndSendQuestionsDto productId);
        Task<Responder<string>> EditProductQuestionAnswers(List<EditProductQuestionAnswerDto> dto);
    }
}
