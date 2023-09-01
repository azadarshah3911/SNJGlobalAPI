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
using System.Diagnostics.Eventing.Reader;

namespace SNJGlobalAPI.Repositories.ProductionRepos
{
    public class QARepo : IQA
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public QARepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }

        public async  Task<Responder<object>> AddQAAsync(AddQaDto dto)
        {
            if (!await _db.IsAnyAsync<Lead>(w => w.ID == dto.Fk_LeadID))
                return Rr.NotFound<object>("Lead", dto.Fk_LeadID.ToString());

            var tran = await _db.BeginTranAsync();

            int? createdBy = JwtHandlerRepo.GetCrntUserId(httpContext);
            
            var map = _mapper.Map<QA>(dto);
            map.FK_CreatedBy = createdBy;
            
            if (!await _db.PostAsync(map))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }

            if (dto.QaFiles is not null)
            {
                var files = new List<QAFiles>();
                foreach (var item in dto.QaFiles)
                {
                    var path = await UploadFiles.SaveAsync(item, "QA");
                    if (path is null)
                        return Rr.Fail<object>("Create");

                    files.Add(new()
                    {
                        CreatedBy = createdBy,
                        FK_QAId = map.ID,
                        File = $"{path.folderPath}/{path.fileName}",
                        FileType = path.FileType,
                    });


                }
                if (!await _db.PostRangeAsync(files))
                {
                    await tran.RollbackAsync();
                    return Rr.Fail<object>("Create");
                }
            }

            var status = new LeadStatus()
            {
                FK_CreatedBy = createdBy,
                FK_LeadId = dto.Fk_LeadID,
                FK_StatusId = await SetStatus(dto.Fk_StatusId ?? 0, dto.Fk_LeadID ?? 0,dto.CurrentevStatus ?? 0)
            };

            //SAving Status For Stage 3
            if (!await _db.PostAsync<LeadStatus>(status))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }

            //Agent Penalty If Any?
            if (dto.Penalty is not null && dto.Penalty > 0)
            {
                if (!await _db.PostAsync<AgentPenalty>(new()
                {
                    Amount = (int)dto.Penalty,
                    Fk_PenaltyFrom = createdBy,
                    Fk_PenaltyTo = dto.AgentId,
                    Fk_LeadID = dto.Fk_LeadID,
                    Fk_StageId = 4,
                    Reason = dto.PenaltyReason,
                }))
                {
                    await tran.RollbackAsync();
                    return Rr.Fail<object>("Create");
                }
            }

            List<QaQuestionAnswer> qaQuestionAnswers = new();

            foreach (var item in dto.QuestionAnswers)
            {
                qaQuestionAnswers.Add(new()
                {
                    Answer = item.Answer,
                    FK_QaID = map.ID,
                    FK_QuestionID = item.FK_QuestionID,
                    FK_CreatedBy = createdBy
                });
               
            }
            //This Was Modify Cause Loop Is Doing Slow
            if (!await _db.PostRangeAsync(qaQuestionAnswers))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }
            
            await tran.CommitAsync();
            return Rr.Success<object>("Created", map.ID);
        }

        public async Task<Responder<List<GetQAListDto>>> GetAllAsync(SearchDto dto)
        {
            var data = await _db.GetAllByAsync<Lead, GetQAListDto>(QaMapper.GetQAList,w => w.Fk_StatusId == 3 || w.Fk_StatusId == 18);
            return Rr.SuccessFetch(data);
        }

        public async Task<Responder<List<GetQAListForAgentDto>>> GetAllForAgentAsync(SearchDto dto)
        {
            int? agent = JwtHandlerRepo.GetCrntUserId(httpContext);
            var data = await _db.GetAllByAsync<LeadAssigned, GetQAListForAgentDto>(QaMapper.GetQAListForAgent, 
                w => w.FK_AgentId == agent &&
                (w.Lead.Fk_StatusId == 3 || w.Lead.Fk_StatusId == 14)
            );
            return Rr.SuccessFetch(data);
        }

        public async Task<Responder<GetQaDetailDto>> GetByLeadIdAsync(int leadId)
        {
            var data = await _db.GetByAsync<Lead, GetQaDetailDto>(w => w.ID == leadId, QaMapper.GetDetails);
            var getProductId = await _db.GetAllByAsync<LeadSubProduct, GetSelectedProductDto>(QaMapper.GetLeadProductId,
                w => w.FK_LeadID == leadId /*&& w.IsApproved*/
                );
            List<GetProductQuestionForQaDto> questions = new();
            foreach (var item in getProductId.DistinctBy(s => s.ID).ToList())
            {
                data.QaQuestions.AddRange( await _db.GetAllByAsync<ProductQuestion, GetProductQuestionForQaDto>(QaMapper.GetQuestionsForQa,w => w.FK_ProductId == item.ID && w.IsActive && w.FK_StageId == 4)
                ) ;
            }
            return Rr.SuccessFetch(data);
        }

        private async Task<int> SetStatus(int status,int leadId,int evStatus)
        {
            if (status == 25)
                return 25;
            else if (status == 27)
                return 27;
            else if (status == 14)
                return 18; //18 QA Re-Examine
            else if(await CheckCovidLead(leadId) && status != 14 && evStatus == 5)
                return 34; //23 Full Fill
            else if(evStatus != 5)
                return 35; //35 Other Plan
            else
                return 28; //28 Call Verification Pendingg

        }

        private async Task<bool> CheckCovidLead(int leadid)
        => await _db.IsAnyAsync<Lead>(predictae => predictae.ID == leadid && predictae.LeadSubProducts.Any(product => product.FK_SubProductId == 1));

        public async Task<Responder<List<GetQAListDto>>> GetAllNotQualifiedAsync(SearchDto dto)
        {
            var data = await _db.GetAllByAsync<Lead, GetQAListDto>(QaMapper.GetQAList, w => w.Fk_StatusId == 27);
            return Rr.SuccessFetch(data);
        }

        public async Task<Responder<List<GetQAListDto>>> GetAllOtherPlansAsync(SearchDto dto)
        {
            var data = await _db.GetAllByAsync<Lead, GetQAListDto>(QaMapper.GetQAList, w => w.Fk_StatusId == 35);
            return Rr.SuccessFetch(data);
        }
    }
}
