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
    public class PatientRepo : IPatient
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public PatientRepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }

        public async Task<Responder<object>> AddPatientAsync(AddPatientDto dto)
        {
            var ins = await _db.GetAsync<Patient>(w => w.MedicareID == dto.MedicareID);
            if (ins is not null)
                return Rr.Exists<object>("Medicare ID", dto.MedicareID);

            if (dto is null)
                return Rr.InvalidModel<object>();
            var map = _mapper.Map<Patient>(dto);
            map.CreatedAt = DateTime.UtcNow;
            map.FK_CreatedBy = JwtHandlerRepo.GetCrntUserId(httpContext);
            if (!await _db.PostAsync(map))
                return Rr.Fail<object>("Create");

            return Rr.Success<object>("Created", map.ID);
        }

        public async Task<Responder<object>> DeletePatientAsync(int id)
        {
            //get instructor first
            var ins = await _db.GetAsync<Patient>(w => w.ID == id && w.DeletedAt == null);
            if (ins is null)
                return Rr.NotFound<object>("Patient", id.ToString());

            //now mark delete
            ins.DeletedAt = DateTime.UtcNow;
            ins.FK_DeletedBy = JwtHandlerRepo.GetCrntUserId(httpContext);

            //now update means mark delete
            if (!await _db.UpdateAsync(ins))
                return Rr.Fail<object>("delete");

            return Rr.Success<object>("deleted");
        }

        public async Task<Responder<List<GetPatientDto>>> GetAllPatientAsync(SearchDto search)
        {
            var ins = await _db.GetAllByAsync<Patient, GetPatientDto>(
                          Map.MapPatientGet(),
                          w => w.DeletedAt == null && (w.FirstName.Contains(search.search) ||
                             w.MiddleName.Contains(search.search) ||
                             w.LastName.Contains(search.search) ||
                             w.PhoneNumber.Contains(search.search) ||
                             w.Address.Contains(search.search) ||
                             w.Address2.Contains(search.search) ||
                             w.City.Contains(search.search) ||
                             w.Suffix.Contains(search.search) ||
                             w.Gender.Contains(search.search) ||
                             w.ZipCode.Contains(search.search)),
                          page: search.page);

            if (ins is null || ins.Count() < 1)
                return Rr.NoData(ins);

            return Rr.SuccessFetch(ins);
        }

        public async Task<Responder<GetPatientDto>> GetPatientAsync(int id)
        {
            var ins = await _db.GetByAsync<Patient, GetPatientDto>
                 (w => w.ID == id && w.DeletedAt == null, Map.MapPatientGet());

            if (ins is null)
                return Rr.NotFound<GetPatientDto>("Patient", id.ToString());

            return Rr.SuccessFetch(ins);
        }

        public async Task<Responder<string>> UpdatePatientAsync(UpdatePatientDto dto,int stageId,int leadId)
        {
            var data = await _db.GetAsync<Patient>(w => w.ID == dto.ID);
            if (data is null)
                return Rr.NotFound<string>("Patient", dto.ID.ToString());

            var user = JwtHandlerRepo.GetCrntUserId(httpContext);
            var tran = await _db.BeginTranAsync();

            var map = _mapper.Map<PatientLogs>(data);
            map.ID = 0;
            map.FK_PatientID = dto.ID;
            map.Fk_StageId = stageId;
            map.Fk_LeadId = leadId;
            map.FK_CreatedBy = user;

            var answerid = dto.QuestionAnswer.Select(selector => selector.Id).ToList();
            var quesdata = await _db.GetAllAsync<ProductQuestionAnswer>(predicate => answerid.Contains(predicate.ID));
            foreach (var item in dto.QuestionAnswer)
            {
                var edit = quesdata.Where(predicate => predicate.ID == item.Id).FirstOrDefault();
                edit.Answer = item.Answer;

                if (!await _db.UpdateAsync(edit))
                {
                    await tran.RollbackAsync();
                    return Rr.Fail<string>("updated");
                }

            }

            if (data.MedicareID != dto.MedicareID && !await _db.IsAnyAsync<Patient>(w => w.MedicareID == dto.MedicareID))
            {
                var mapPatient = _mapper.Map<Patient>(dto);
                mapPatient.ID = 0;
                if(!await _db.PostAsync<Patient>(mapPatient))
                {
                    await tran.RollbackAsync();
                    return Rr.Fail<string>("update");
                }
                
                if (!await _db.SqlQueryAsync($"UPDATE Leads SET Fk_PatientId={mapPatient.ID} WHERE ID={leadId}"))
                {
                    await tran.RollbackAsync();
                    return Rr.Fail<string>("update");
                }
                map.FK_PatientID = mapPatient.ID;

                if (!await _db.PostAsync<PatientLogs>(map))
                    return Rr.Fail<string>("Create");

                await tran.CommitAsync();
                return Rr.Success<string>("Created", data.ID.ToString());
            }

            if (data.MedicareID != dto.MedicareID && await _db.IsAnyAsync<Patient>(w => w.MedicareID == dto.MedicareID))
                return Rr.Custom<string>($"medicare Id already exists {dto.MedicareID}");


           
            data.FK_UpdatedBy = user;
            data.UpdatedAt = DateTime.UtcNow;
            data.FK_StateId = dto.FK_StateId;
            data.FirstName = dto.FirstName;
            data.LastName = dto.LastName;
            data.MiddleName = dto.MiddleName;
            data.Suffix = dto.Suffix;
            data.Address = dto.Address;
            data.Address2 = dto.Address2;
            data.City = dto.City;
            data.ZipCode = dto.ZipCode;
            data.Gender = dto.Gender;
            data.DateofBirth = dto.DateofBirth;
            data.PhoneNumber = dto.PhoneNumber;
            data.MedicareID = dto.MedicareID;


            if (!await _db.PostAsync<PatientLogs>(map))
                return Rr.Fail<string>("Create");

            if (!await _db.UpdateAsync(data))
            {
                await tran.RollbackAsync();
                return Rr.Fail<string>("update");

            }
           
            await tran.CommitAsync();
            return Rr.Success<string>("Updated" , data.ID.ToString());

        }

        public async Task<Responder<GetPatientFoAgentDto>> GetPatientByMedicarIdAsync(string medicareid)
        {
            var ins = await _db.GetByAsync<Patient, GetPatientFoAgentDto>
            (w => w.MedicareID.ToLower() == medicareid.ToLower() && w.DeletedAt == null,Map.MapPatientForAgentGet());

            if (ins is null)
                return Rr.NotFound<GetPatientFoAgentDto>("Patient", medicareid.ToString());
            return Rr.SuccessFetch(ins);
        }

        public async Task<Responder<GetPatientForAgenBySsntDto>> GetPatientBySsnAsync(string ssn)
        {
            var ins = await _db.GetByAsync<Patient, GetPatientForAgenBySsntDto>
            (w => w.Ssn.ToLower() == ssn.ToLower() && w.DeletedAt == null, Map.MapPatientForAgentByssnGet());

            if (ins is null)
                return Rr.NotFound<GetPatientForAgenBySsntDto>("Patient", ssn.ToString());
            return Rr.SuccessFetch(ins);
        }

        public async Task<Responder<List<PatientLeadListDto>>> GetPatientLeadCountAsync()
        {
            var ins = await _db.GetAllByAsync<Patient, PatientLeadListDto>(Map.MapPatientLeadGet());

            if (ins is null || ins.Count() < 1)
                return Rr.NoData(ins);

            return Rr.SuccessFetch(ins);
        }


        public async Task<Responder<GetEditPatientForAgentDto>> GetEditPatientAsync(int leadId)
        {
            if (httpContext.User.IsInRole("Agent"))
            {
                var data = await _db.GetByAsync<Lead, GetEditPatientForAgentDto>
               (w => w.ID == leadId && w.CreatedAt.Date == DateTime.UtcNow.Date, LeadMapper.GetEditPatient);
                if (data is null)
                    return Rr.NotFound<GetEditPatientForAgentDto>("Lead", leadId.ToString());
                
                return Rr.SuccessFetch(data);
            }

            var ins = await _db.GetByAsync<Lead, GetEditPatientForAgentDto>
           (w => w.ID == leadId, LeadMapper.GetEditPatient);

            if (ins is null)
                return Rr.NotFound<GetEditPatientForAgentDto>("Lead", leadId.ToString());
            return Rr.SuccessFetch(ins);
        }
    }
}
