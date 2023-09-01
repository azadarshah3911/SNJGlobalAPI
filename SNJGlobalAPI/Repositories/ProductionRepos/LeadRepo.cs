using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class LeadRepo : ILead
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public LeadRepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }
        public async Task<Responder<object>> AddLeadAsync(AddLeadDto dto)
        {
            var tran = await _db.BeginTranAsync();
            bool isDuplicate = false;

            int? createdBy = JwtHandlerRepo.GetCrntUserId(httpContext);
            
            if (dto.PatientID is null || dto.PatientID <= 0)
            {
                var map = _mapper.Map<Patient>(dto);
                map.FK_CreatedBy = createdBy;

                if (!await _db.PostAsync(map)) return Rr.Fail<object>("Create");
                dto.PatientID = map.ID;
            }
            else
            {
                //Have To Add Logic For Same Product
                isDuplicate = await MarkLeadAsDuplicate(new()
                {
                    ID = dto.PatientID ?? 0,
                    SubProductId = dto.SubProducts.FirstOrDefault()
                });

            }

            //Generating Lead Here
            var leadDto = new Lead()
            {
                Fk_PatientId = dto.PatientID,
                Notes = dto.Notes,
                OnBehalf = dto.OnBehalf,
                FK_CreatedBy = createdBy,
                IsDuplicate = isDuplicate
            };

            if (!await _db.PostAsync(leadDto))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }
            //Saving Lead Status
            if(!await AddLeadStatusAsync(leadDto.ID, createdBy))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }
            //Saving Products Here
            var subProductsList = new List<LeadSubProduct>();
            foreach (int subProduct in dto.SubProducts)
            {
                subProductsList.Add(new()
                {
                    FK_LeadID = leadDto.ID,
                    FK_CreatedBy = createdBy,
                    FK_SubProductId = subProduct,
                    FK_StagetId = 1,
                    StageCount = 1,
                });
            }
            if (!await _db.PostRangeAsync(subProductsList))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }
            //Saving Questions
            var questionList = new List<ProductQuestionAnswer>();
            foreach (var ques in dto.Questions)
            {
                questionList.Add(new()
                {
                    FK_LeadId = leadDto.ID,
                    FK_CreatedBy = createdBy,
                    Answer = ques.Answer,
                    FK_QuestionId = ques.QuestionId,
                });
            }
            if (!await _db.PostRangeAsync(questionList))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }
            await tran.CommitAsync();
            return Rr.Success<object>("Created", leadDto.ID);
        }

        public async Task<Responder<GetLeadDetailsDto>> GetLeadByIdAsync(int id)
        {
            var data = await _db.GetByAsync<Lead, GetLeadDetailsDto>(w => w.ID == id,LeadMapper.GetLeadDetails);
            var getProductId = await _db.GetAllByAsync<LeadSubProduct, GetSelectedProductDto>(QaMapper.GetLeadProductId,
               w => w.FK_LeadID == id && w.IsApproved
               );
            List<GetProductQuestionForQaDto> questions = new();
            foreach (var item in getProductId.DistinctBy(s => s.ID).ToList())
            {
                
                data.QaQuestions.AddRange(await _db.GetAllByAsync<ProductQuestion, GetProductQuestionForQaDto>(QaMapper.GetQuestionsForQa, w => w.FK_ProductId == item.ID && w.IsActive && w.FK_StageId == 4)
                );
            }
            return Rr.SuccessFetch(data);
        }

        //New Lead Status
        private async Task<bool> AddLeadStatusAsync(int leadId,int? user) => await _db.PostAsync<LeadStatus>(new()
        {
            FK_CreatedBy = user,
            FK_LeadId = leadId,
            FK_StatusId = 1 //New Lead Status On 1 In DB
        });

        /* public async Task<Responder<object>> DeleteLeadAsync(int id)
         {
             throw new NotImplementedException();
         }

         public async Task<Responder<List<GetProductDto>>> GetAllLeadAsync(SearchDto search)
         {
             throw new NotImplementedException();
         }

         public async Task<Responder<GetProductDto>> GetLeadAsync(int id)
         {
             throw new NotImplementedException();
         }

         public async Task<Responder<object>> UpdateLeadAsync(ProductUpdateDto dto)
         {
             throw new NotImplementedException();
         }*/
    
        public async Task<Responder<List<leadListDto>>> GetAllLeadAsync()
        {
            Expression<Func<Lead,bool>> predicate = null;
            var currentDate = DateTime.UtcNow.Date;
            var branch = await _db.GetByAsync<User, GetUserBranchDto>(wherePredicate => wherePredicate.ID == JwtHandlerRepo.GetCrntUserId(httpContext), UserMapper.GetUserBranch);

            if (httpContext.User.IsInRole("Agent"))
                predicate = where => where.FK_CreatedBy == JwtHandlerRepo.GetCrntUserId(httpContext) && where.CreatedAt.Date == currentDate;

            else if (httpContext.User.IsInRole("Production Manager"))
                predicate = where => where.CreatedBy.branch.Name == branch.Name && where.CreatedAt.Date >= currentDate.AddDays(-31).Date && where.CreatedAt.Date <= currentDate;

            else if (httpContext.User.IsInRole("Team Lead"))
                predicate = where => where.CreatedBy.branch.Name == branch.Name && where.CreatedAt.Date >= currentDate.AddDays(-7).Date && where.CreatedAt.Date <= currentDate;

            else if (httpContext.User.IsInRole("Chassing Agent") || httpContext.User.IsInRole("Qa Agent"))
                    return Rr.NoData(new List<leadListDto>());

            else if (httpContext.User.IsInRole("Qa Manager"))
                predicate = where => /*(where.Fk_StatusId == 28 || where.Fk_StatusId == 15) &&*/ where.CreatedAt.Date >= currentDate.AddDays(-31).Date && where.CreatedAt.Date <= currentDate;

            var ins = await _db.GetAllByAsync<Lead, leadListDto>(LeadMapper.GetLeadList, predicate);

            
            if (ins is null || ins.Count() < 1)
                return Rr.NoData(ins);

            return Rr.SuccessFetch(ins);
        }

        private async Task<bool> MarkLeadAsDuplicate(CheckPatientSameSubProductDto dto)
        {

            CheckPatientSameProductDto patient = await _db.GetByAsync<Patient, CheckPatientSameProductDto>((predictae) => predictae.ID == dto.ID,Map.CheckPatientSameProduct());
            return patient.ProductId.Where((predicate) => predicate == dto.SubProductId).Any();
        }
        /*
                var check = await _db.Db().Patients.Include((include) => include.Leads).ThenInclude((thenInclude) => thenInclude.LeadSubProducts).Where(predictae => predictae.ID == dto.ID).Select(patient => new
                {
                    ID = patient.ID,
                    ProductId = patient.Leads.SelectMany(lead => lead.LeadSubProducts)
                                              .Select(subProduct => subProduct.FK_SubProductId)
                                              .ToList()
                }).FirstOrDefaultAsync();*/



        public async Task<Responder<List<leadListDto>>> GetAllRejectedAsync()
        {
            Expression<Func<Lead, bool>> predicate = where => where.QA.OrderByDescending(order => order.ID).FirstOrDefault().Fk_StatusId == 27;
            var currentDate = DateTime.UtcNow.Date;
            var branch = await _db.GetByAsync<User, GetUserBranchDto>(wherePredicate => wherePredicate.ID == JwtHandlerRepo.GetCrntUserId(httpContext), UserMapper.GetUserBranch);

            if (httpContext.User.IsInRole("Agent"))
                predicate = where => where.QA.OrderByDescending(order => order.ID).FirstOrDefault().Fk_StatusId == 27 && where.FK_CreatedBy == JwtHandlerRepo.GetCrntUserId(httpContext) && where.CreatedAt.Date >= currentDate && where.CreatedAt.Date <= currentDate.AddDays(-31);

            else if (httpContext.User.IsInRole("Production Manager"))
                predicate = where => where.QA.OrderByDescending(order => order.ID).FirstOrDefault().Fk_StatusId == 27 && where.CreatedBy.branch.Name == branch.Name && where.CreatedAt.Date >= currentDate && where.CreatedAt.Date <= currentDate.AddDays(-31);

            else if (httpContext.User.IsInRole("Team Lead"))
                predicate = where => where.QA.OrderByDescending(order => order.ID).FirstOrDefault().Fk_StatusId == 27 && where.CreatedBy.branch.Name == branch.Name && where.CreatedAt.Date >= currentDate && where.CreatedAt.Date <= currentDate.AddDays(-31);

            else if (httpContext.User.IsInRole("Chassing Agent") || httpContext.User.IsInRole("Qa Agent"))
                return Rr.NoData(new List<leadListDto>());

            var ins = await _db.GetAllByAsync<Lead, leadListDto>(LeadMapper.GetLeadList, predicate);


            if (ins is null || ins.Count() < 1)
                return Rr.NoData(ins);

            return Rr.SuccessFetch(ins);
        }


        public async Task<Responder<object>> DeleteLeadAsync(int leadId)
        {
            var data = await _db.GetAsync<Lead>(w => w.ID == leadId);
            data.FK_DeletedBy = JwtHandlerRepo.GetCrntUserId(httpContext);
            data.DeletedAt = DateTime.UtcNow;
            if(!await _db.UpdateAsync(data))
                return Rr.Fail<object>("Created", leadId.ToString());
            
            return Rr.Success<object>("Created", leadId);
        }

    }
}
