using AutoMapper;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DbModelsProduction;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.GeneralServices;
using SNJGlobalAPI.Mappers;
using SNJGlobalAPI.Repositories.CommonInterfaces;
using SNJGlobalAPI.Repositories.ProductionInterfaces;
using SNJGlobalAPI.SecurityHandlers;

namespace SNJGlobalAPI.Repositories.ProductionRepos
{
    public class ErrorLeadRepo : IErrorLeads
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public ErrorLeadRepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }

        public async Task<Responder<List<GetErrorLeadListDto>>> GetAllErrorLeads()
        {
            var data = await _db.GetAllByAsync<Lead, GetErrorLeadListDto>(ErrorLeadMapper.GetErrorList,
                w => w.Fk_StatusId == 4 || w.Fk_StatusId == 24 || w.Fk_StatusId == 25
            );
            return Rr.SuccessFetch(data);
        }
    }
}
