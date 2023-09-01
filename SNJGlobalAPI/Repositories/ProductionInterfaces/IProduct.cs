using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Repositories.Interfaces
{
    public interface IProduct
    {
        Task<Responder<object>> AddProductAsync(AddProductDto dto);
        Task<Responder<GetProductDto>> GetProductAsync(int id);
        Task<Responder<List<GetProductDto>>> GetAllProductAsync(SearchDto search);
        Task<Responder<object>> DeleteProductAsync(int id);
        Task<Responder<object>> UpdateProductAsync(ProductUpdateDto dto);
        Task<Responder<List<GetSubProductForAgentDto>>> GetAllSubProductForAgentAsync();

        Task<Responder<List<GetSubProductForAgentDto>>> GetAllSubProductForSnsAsync(int leadId);
    }
}
