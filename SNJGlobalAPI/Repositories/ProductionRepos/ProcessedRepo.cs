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
    public class ProcessedRepo : IProcessed
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public ProcessedRepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }

        public async Task<Responder<object>> AddChassinAsync(AddChassingDto dto)
        {
            if (!await _db.IsAnyAsync<Lead>(w => w.ID == dto.Fk_LeadId))
                return Rr.NotFound<object>("Lead", dto.Fk_LeadId.ToString());

            var tran = await _db.BeginTranAsync();

            int? createdBy = JwtHandlerRepo.GetCrntUserId(httpContext);

            var map = _mapper.Map<Chassing>(dto);
            map.FK_CreatedBy = createdBy;

            if (!await _db.PostAsync(map))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }

            var status = new LeadStatus()
            {
                FK_CreatedBy = createdBy,
                FK_LeadId = dto.Fk_LeadId,
                FK_StatusId = SetStatus(dto.Fk_StatusId ?? 0)
            };
            
            //SAving Status For Stage 6
            if (!await _db.PostAsync(status))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }

            if (dto.ChassingFiles is not null)
            {
                var files = new List<ChassingFile>();
                foreach (var item in dto.ChassingFiles)
                {
                    var path = await UploadFiles.SaveAsync(item, "Chassing");
                    if (path is null)
                        return Rr.Fail<object>("Create");

                    files.Add(new()
                    {
                        FK_CreatedBy = createdBy,
                        FK_LeadID = map.Fk_LeadId,
                        File = $"{path.folderPath}/{path.fileName}",
                        FileType = path.FileType,
                        Fk_ChassingId = map.Id,
                    });
                    
                }
                if (!await _db.PostRangeAsync(files))
                {
                    await tran.RollbackAsync();
                    return Rr.Fail<object>("Create");
                }
            }

            await tran.CommitAsync();
            return Rr.Success<object>("Created", map.Id);

        }

        private int SetStatus(int status)
        {
            if (status == 20) //20 Pending
                return 15; //15 Chassing Pending
            else if (status == 21 && httpContext.User.IsInRole("Chassing Agent"))
                return 31;
            else if (status == 21 && !httpContext.User.IsInRole("Chassing Agent"))
                return 23;
            else if (status == 36)
                return 3;
            else if (status == 37)
                return 38;
            else
                return 26;
        }


        public async Task<Responder<List<GetQAListDto>>> GetAllCgmLeadAsync(SearchDto dto)
        {
            var data = await _db.GetAllByAsync<Lead, GetQAListDto>(ProcessedMapper.GetProcessedList, w => (w.Fk_StatusId == 15 || w.Fk_StatusId == 31 || w.Fk_StatusId == 38) && w.LeadSubProducts.FirstOrDefault().SubProduct.Product.Name.ToLower() == "cgm");
            return Rr.SuccessFetch(data);
        }

        public async Task<Responder<List<GetQAListDto>>> GetAllBracesLeadAsync(SearchDto dto)
        {
            var data = await _db.GetAllByAsync<Lead, GetQAListDto>(ProcessedMapper.GetProcessedList, w => (w.Fk_StatusId == 15 || w.Fk_StatusId == 31 || w.Fk_StatusId == 38) && w.LeadSubProducts.FirstOrDefault().SubProduct.Product.Name.ToLower() == "braces");
            return Rr.SuccessFetch(data);
        }

        public async Task<Responder<List<GetQAListForAgentDto>>> GetBracesForAgent()
        {
            int? agent = JwtHandlerRepo.GetCrntUserId(httpContext);
            var data = await _db.GetAllByAsync<LeadAssigned, GetQAListForAgentDto>(ProcessedMapper.GetProcessedListForAgent,
                w => w.FK_AgentId == agent &&
                (w.Lead.Fk_StatusId == 15 || w.Lead.Fk_StatusId == 20 || w.Lead.Fk_StatusId == 38) && w.Lead.LeadSubProducts.FirstOrDefault().SubProduct.Product.Name.ToLower() == "braces"
            );
            return Rr.SuccessFetch(data);
        }

        public async Task<Responder<List<GetQAListForAgentDto>>> GetCgmForAgent()
        {
            int? agent = JwtHandlerRepo.GetCrntUserId(httpContext);
            var data = await _db.GetAllByAsync<LeadAssigned, GetQAListForAgentDto>(ProcessedMapper.GetProcessedListForAgent,
                w => w.FK_AgentId == agent &&
                (w.Lead.Fk_StatusId == 15 || w.Lead.Fk_StatusId == 20 || w.Lead.Fk_StatusId == 38) && w.Lead.LeadSubProducts.FirstOrDefault().SubProduct.Product.Name.ToLower() == "cgm"
            );
            return Rr.SuccessFetch(data);
        }

        public async Task<Responder<GetProcessedDetailsDto>> GetByLeadIdAsync(int leadId)
        {
            var data = await _db.GetByAsync<Lead, GetProcessedDetailsDto>(w => w.ID == leadId, ProcessedMapper.GetDetails);

            var getProductId = await _db.GetAllByAsync<LeadSubProduct, GetSelectedProductDto>(QaMapper.GetLeadProductId,
                w => w.FK_LeadID == leadId && w.IsApproved
                );
            List<GetProductQuestionForQaDto> questions = new();
            foreach (var item in getProductId.DistinctBy(s => s.ID).ToList())
            {
                data.QaQuestions.AddRange(await _db.GetAllByAsync<ProductQuestion, GetProductQuestionForQaDto>(QaMapper.GetQuestionsForQa, w => w.FK_ProductId == item.ID && w.IsActive && w.FK_StageId == 4)
                );
            }
            return Rr.SuccessFetch(data);
        }


        public async Task<Responder<List<GetQAListDto>>> GetDeniedLeadsAsync()
        {
            var data = await _db.GetAllByAsync<Lead, GetQAListDto>(ProcessedMapper.GetProcessedList, w => w.Fk_StatusId == 26);
            return Rr.SuccessFetch(data);
        }

        public async Task<Responder<List<GetQAListForAgentDto>>> GetDeniedForAgentAsync()
        {
            var crnt = DateTime.UtcNow;

            int? agent = JwtHandlerRepo.GetCrntUserId(httpContext);
            var data = await _db.GetAllByAsync<LeadAssigned, GetQAListForAgentDto>(ProcessedMapper.GetProcessedListForAgent,
                w => w.FK_AgentId == agent && w.FK_StageId == 5 && w.Lead.Fk_StatusId == 26 
                && w.CreatedAt.Date > crnt.AddDays(-30).Date && w.CreatedAt.Date < crnt.Date
            );
            return Rr.SuccessFetch(data);
        }

    }
}
