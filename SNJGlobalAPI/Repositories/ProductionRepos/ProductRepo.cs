using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.GeneralServices;
using SNJGlobalAPI.Repositories.CommonInterfaces;
using SNJGlobalAPI.Repositories.Interfaces;
using SNJGlobalAPI.SecurityHandlers;
using System.Xml.Linq;

namespace SNJGlobalAPI.Repositories.Repos
{
    public class ProductRepo : IProduct
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public ProductRepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }
        public async Task<Responder<object>> AddProductAsync(AddProductDto dto)
        {
            if (dto is null)
                return Rr.InvalidModel<object>();

            var map = _mapper.Map<Product>(dto);
            var user = JwtHandlerRepo.GetCrntUserId(httpContext);
            map.FK_CreatedBy = user;
            var tran = await _db.BeginTranAsync();
            if (!await _db.PostAsync(map))
            {

                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }
            if (!await _db.PostAsync<SubProduct>(new()
            {
                Name = map.Name,
                Code = map.Name,
                CreatedAt = DateTime.UtcNow,
                FK_CreatedBy = user,
                FK_ProductId = map.ID,
                IsParent = true
            }))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }
            await tran.CommitAsync();
            return Rr.Success<object>("Created", map.ID);
        }

        public async Task<Responder<GetProductDto>> GetProductAsync(int id)
        {
            var pro = await _db.GetByAsync<Product, GetProductDto>
                (w => w.ID == id && w.DeletedAt == null, Map.MapProductGet());

            if (pro is null)
                return Rr.NotFound<GetProductDto>("Product", id.ToString());

            return Rr.SuccessFetch(pro);
        }

        public async Task<Responder<List<GetProductDto>>> GetAllProductAsync(SearchDto search)
        {
            var ins = await _db.GetAllByAsync<Product, GetProductDto>(
                        Map.MapProductGet(),
                        w => w.DeletedAt == null && (w.Name.Contains(search.search)),
                        page: search.page);

            if (ins is null || ins.Count() < 1)
                return Rr.NoData(ins);

            return Rr.SuccessFetch(ins);
        }

        //Just Added But Not Created Relation Of FK_Deleted
        public async Task<Responder<object>> DeleteProductAsync(int id)
        {
            //get instructor first
            var ins = await _db.GetAsync<Product>(w => w.ID == id && w.DeletedAt == null);
            if (ins is null)
                return Rr.NotFound<object>("Product", id.ToString());

            //now mark delete
            ins.DeletedAt = DateTime.UtcNow;
         /*   ins.Fk_DeletedBy = JwtHandlerRepo.GetCrntUserId(httpContext);
*/
            //now update means mark delete
            if (!await _db.UpdateAsync(ins))
                return Rr.Fail<object>("delete");

            return Rr.Success<object>("deleted");
        }
        //Just Added Not Created Relation for UpdatedBy
        public async Task<Responder<object>> UpdateProductAsync(ProductUpdateDto dto)
        {
            //verify is exists
            var ins = await _db.GetAsync<Product>(w => w.ID == dto.Id && w.DeletedAt == null);

            if (ins is null)
                return Rr.NotFound<object>("Product", dto.Id.ToString());

            var id = new SqlParameter("@Id", dto.Id);
            var name = new SqlParameter("@Name", dto.Name);
            var fk_updateBy = new SqlParameter("@FK_Update", JwtHandlerRepo.GetCrntUserId(httpContext));

            string query = $"UPDATE [E].[Products] SET [Name]=@Name, [UpdatedOn]=GETDATE(),[Fk_UpdatedBy]=@FK_Update WHERE Id=@Id";

            if (!await _db.SqlQueryAsync(query, name, fk_updateBy, id))
                return Rr.Fail<object>("Update");

            return Rr.Success<object>("updated");

        }

        public async Task<Responder<List<GetSubProductForAgentDto>>> GetAllSubProductForAgentAsync()
        {
            var ins = await _db.GetAllByAsync<SubProduct, GetSubProductForAgentDto>(predicate:w => w.IsParent);

            if (!ins.Any())
                return Rr.NoData(ins);

            return Rr.SuccessFetch(ins);
        }

        public async Task<Responder<List<GetSubProductForAgentDto>>> GetAllSubProductForSnsAsync(int leadId)
        {
            var ins = await _db.GetAllByAsync<SubProduct, GetSubProductForAgentDto>(predicate: w => !w.IsParent);

            if (!ins.Any())
                return Rr.NoData(ins);

            return Rr.SuccessFetch(ins);
        }
    }
}
