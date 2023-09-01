using AutoMapper;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.GeneralServices;
using SNJGlobalAPI.Mappers;
using SNJGlobalAPI.Repositories.CommonInterfaces;
using SNJGlobalAPI.Repositories.ProductionInterfaces;
using SNJGlobalAPI.SecurityHandlers;
using System;
using System.Linq.Expressions;

namespace SNJGlobalAPI.Repositories.ProductionRepos
{
    public class EligibilityRepo : IEligibility
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public EligibilityRepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }

        public async Task<Responder<object>> AddEligibilityAsync(AddEligibilityDto dto)
        {
            var tran = await _db.BeginTranAsync();

            int? createdBy = JwtHandlerRepo.GetCrntUserId(httpContext);
            // Save File If Any?
            if (dto.EligibilityFile is not null)
            {
                var path = await UploadFiles.SaveAsync(dto.EligibilityFile, "Eligibility");
                if (path is null)
                    return Rr.Fail<object>("Create");

                if (!await _db.PostAsync<LeadFile>(new()
                {
                    CreatedBy = createdBy,
                    FK_StageId = 2,
                    FK_LeadID = dto.FK_LeadID,
                    File = $"{path.folderPath}/{path.fileName}",
                    FileType = path.FileType,
                    
                }))
                {
                    await tran.RollbackAsync();
                    return Rr.Fail<object>("Create");
                }
            }
           

            var map = _mapper.Map<Eligibility>(dto);
            map.CreatedBy = createdBy;

            //Add Eligibility Verification
            if (!await _db.PostAsync(map))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create"); 
            }

            //Agent Penalty If Any?
            if (dto.Penalty is not null && dto.Penalty > 0)
            {
                if (!await _db.PostAsync<AgentPenalty>(new()
                {
                    Amount = (int) dto.Penalty,
                    Fk_PenaltyFrom = createdBy,
                    Fk_PenaltyTo = dto.AgentId,
                    Fk_LeadID = dto.FK_LeadID,
                    Fk_StageId = 2,
                    Reason = dto.PenaltyReason,
                }))
                {
                    await tran.RollbackAsync();
                    return Rr.Fail<object>("Create");
                }
            }
            //If Not Ev Error
            if (!await _db.PostAsync<LeadStatus>(new()
            {
                FK_CreatedBy = createdBy,
                FK_LeadId = dto.FK_LeadID,
                FK_StatusId = await UpdateEligibilityStatus(dto.FK_StatusId ?? 0),
            }))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }


            await tran.CommitAsync();
            return Rr.Success<object>("Created", map.ID);
        }

        
        public async Task<Responder<List<GetNewLeadListDto>>> GetAllNewLeadsAsync(SearchDto search) 
        {
            Expression<Func<Lead,bool>> predicate = null;
            var currentDate = DateTime.UtcNow.Date;
            var branch = await _db.GetByAsync<User, GetUserBranchDto>(wherePredicate => wherePredicate.ID == JwtHandlerRepo.GetCrntUserId(httpContext), UserMapper.GetUserBranch);

            if (httpContext.User.IsInRole("Production Manager"))
                predicate = where => where.CreatedBy.branch.Name == branch.Name && (where.Fk_StatusId == 1 || where.Fk_StatusId == 4);

            else if (httpContext.User.IsInRole("Team Lead"))
                predicate = where => where.CreatedBy.branch.Name == branch.Name && where.CreatedAt.Date == currentDate && (where.Fk_StatusId == 1 || where.Fk_StatusId == 4);
            else
                predicate = where => where.Fk_StatusId == 1 || where.Fk_StatusId == 4;

            var data = await _db.GetAllByAsync<Lead, GetNewLeadListDto>(EligibilityMapper.GetNewLeadList,
                predicate,
             orderBy: o => o.ID,
                    IsAsending: false
                    );
            return Rr.SuccessFetch(data);
        }

        public async Task<Responder<List<GetNewLeadListDto>>> GetAllEvErrorsAsync(SearchDto search)
        {
            var data = await _db.GetAllByAsync<Lead, GetNewLeadListDto>(EligibilityMapper.GetNewLeadList, w =>
            w.Fk_StatusId == 4,
             orderBy: o => o.ID,
                    IsAsending: false
                    );
            return Rr.SuccessFetch(data);
        }

        public async Task<Responder<GetNewLeadDetailsDto>> GetByLeadIdAsync(int leadid)
        {
            var data = await _db.GetByAsync<Lead, GetNewLeadDetailsDto>(w => w.ID==leadid && 
            (w.Fk_StatusId == 1 || w.Fk_StatusId == 4) ,EligibilityMapper.GetNewLeadDetails);
            return Rr.SuccessFetch(data);
        }


        public async Task<Responder<object>> EditEligibilityAsync(EditEligibilityDto dto)
        {
            var tran = await _db.BeginTranAsync();

            int? createdBy = JwtHandlerRepo.GetCrntUserId(httpContext);
            // Save File If Any?
            var map = _mapper.Map<Eligibility>(dto);
            map.CreatedBy = createdBy;

            //Add Eligibility Verification
            if (!await _db.PostAsync(map))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }
           
            //If Not Ev Error
            if (!await _db.PostAsync<LeadStatus>(new()
            {
                FK_CreatedBy = createdBy,
                FK_LeadId = dto.FK_LeadID,
                FK_StatusId = await UpdateEligibilityStatus(dto.FK_StatusId ?? 0),
            }))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }


            await tran.CommitAsync();
            return Rr.Success<object>("Created", map.ID);
        }

        public async Task<int> UpdateEligibilityStatus(int status)
        {
            if(status != 4 && status != 5)
                return 3;

            return status == 4 ? 4 : 2;
        }

    }
}
