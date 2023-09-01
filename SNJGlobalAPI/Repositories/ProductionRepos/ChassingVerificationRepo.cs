using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class ChassingVerificationRepo : IChassingVerification
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public ChassingVerificationRepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }

        public async Task<Responder<object>> AddChassingVerificationAsync(AddChassingVerificationDto dto)
        {
            if (!await _db.IsAnyAsync<Lead>(w => w.ID == dto.Fk_LeadId))
                return Rr.NotFound<object>("Lead", dto.Fk_LeadId.ToString());
            var tran = await _db.BeginTranAsync();

            int? createdBy = JwtHandlerRepo.GetCrntUserId(httpContext);

            var map = _mapper.Map<ChassingVerification>(dto);
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
            status.FK_StatusId = dto.Fk_StatusId == 29 ? 15 : 30;
            
            //SAving Status For Stage 6
            if (!await _db.PostAsync(status))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }
            await tran.CommitAsync();
            return Rr.Success<object>("Created", map.Id);
        }

        public async Task<Responder<List<GetQAListDto>>> GetAllLeadAsync(SearchDto dto)
        {
            var data = await _db.GetAllByAsync<Lead, GetQAListDto>(ProcessedMapper.GetProcessedList, w => w.Fk_StatusId == 28);
            return Rr.SuccessFetch(data);
        }

    }
}
