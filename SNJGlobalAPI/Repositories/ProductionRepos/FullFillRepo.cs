using AutoMapper;
using Microsoft.AspNetCore.Http;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.GeneralServices;
using SNJGlobalAPI.Mappers;
using SNJGlobalAPI.Repositories.CommonInterfaces;
using SNJGlobalAPI.Repositories.ProductionInterfaces;
using System.Linq.Expressions;

namespace SNJGlobalAPI.Repositories.ProductionRepos
{
    public class FullFillRepo : IFullFill
    {

        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public FullFillRepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }
        public async Task<Responder<List<leadListDto>>> GetAllAsync()
        {
            Expression<Func<Lead, bool>> predicate = w => w.Fk_StatusId == 34;
            var currentDate = DateTime.UtcNow.Date;
            //var branch = await _db.GetByAsync<User, GetUserBranchDto>(wherePredicate => wherePredicate.ID == JwtHandlerRepo.GetCrntUserId(httpContext), UserMapper.GetUserBranch);

            if (httpContext.User.IsInRole("Chassing Manager"))
                predicate = where => where.Fk_StatusId == 34 && where.CreatedAt.Date >= currentDate && where.CreatedAt.Date == currentDate.AddDays(-30).Date;

            var ins = await _db.GetAllByAsync<Lead, leadListDto>(LeadMapper.GetLeadList, predicate);


            if (ins is null || ins.Count() < 1)
                return Rr.NoData(ins);

            return Rr.SuccessFetch(ins);
        }
    }
}
