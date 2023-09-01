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
using System.Linq.Expressions;

namespace SNJGlobalAPI.Repositories.ProductionRepos
{
    public class ConfirmationRepo : IConfirmation
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public ConfirmationRepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }
        public async Task<Responder<object>> AddAsync(AddConfiramtionDto dto)
        {
            if (!await _db.IsAnyAsync<Lead>(w => w.ID == dto.Fk_LeadId))
                return Rr.NotFound<object>("Lead", dto.Fk_LeadId.ToString());
            var tran = await _db.BeginTranAsync();

            int? createdBy = JwtHandlerRepo.GetCrntUserId(httpContext);

            var map = _mapper.Map<Confirmation>(dto);
            map.Fk_UserId = createdBy;

            if (!await _db.PostAsync(map))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }

            var status = new LeadStatus()
            {
                FK_CreatedBy = createdBy,
                FK_LeadId = dto.Fk_LeadId,
            };
            status.FK_StatusId = dto.Fk_StatusId == 32 ? 34 : 33;

            //SAving Status For Stage 6
            if (!await _db.PostAsync(status))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }
            await tran.CommitAsync();
            return Rr.Success<object>("Created", map.Id);
        }

        public async Task<Responder<List<leadListDto>>> GetAllAsync()
        {
            Expression<Func<Lead, bool>> predicate = w => w.Fk_StatusId == 23;
            var currentDate = DateTime.UtcNow.Date;
            //var branch = await _db.GetByAsync<User, GetUserBranchDto>(wherePredicate => wherePredicate.ID == JwtHandlerRepo.GetCrntUserId(httpContext), UserMapper.GetUserBranch);

            if (httpContext.User.IsInRole("Chassing Manager"))
                predicate = where => where.Fk_StatusId == 23 && where.CreatedAt.Date >= currentDate && where.CreatedAt.Date == currentDate.AddDays(-30).Date;

            var ins = await _db.GetAllByAsync<Lead, leadListDto>(LeadMapper.GetLeadList, predicate);


            if (ins is null || ins.Count() < 1)
                return Rr.NoData(ins);

            return Rr.SuccessFetch(ins);
        }
    }
}
