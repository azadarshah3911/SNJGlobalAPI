using AutoMapper;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;
using SNJGlobalAPI.GeneralServices;
using SNJGlobalAPI.Mappers;
using SNJGlobalAPI.Repositories.CommonInterfaces;
using SNJGlobalAPI.Repositories.ProductionInterfaces;
using SNJGlobalAPI.SecurityHandlers;

namespace SNJGlobalAPI.Repositories.ProductionRepos
{
    public class SNSRepo : ISns
    {
        private readonly IDb _db;
        private readonly IMapper _mapper;
        private readonly HttpContext httpContext;

        public SNSRepo(IDb db, IMapper mapper, IHttpContextAccessor accessor)
        {
            _db = db;
            _mapper = mapper;
            httpContext = accessor.HttpContext;
        }

        public async Task<Responder<object>> AddSnsAsync(AddSnsDto dto)
        {
            if (!await _db.IsAnyAsync<Lead>(w => w.ID == dto.Fk_LeadId))
                return Rr.NotFound<object>("Lead", dto.Fk_LeadId.ToString());

            var tran = await _db.BeginTranAsync();
            int? createdBy = JwtHandlerRepo.GetCrntUserId(httpContext);

            var map = _mapper.Map<SNS>(dto);
            map.FK_CreatedBy = createdBy;

            if (dto.Id > 0)
            {
                var data = await _db.GetAsync<SNS>(w => w.ID == dto.Id);
                data.FK_UpdatedBy = createdBy;
                data.UpdatedAt = DateTime.UtcNow;
                data.Remarks = dto.Remarks;
                data.FK_StatusID = dto.Fk_StatusId;

                if(!await _db.UpdateAsync(data))
                    return Rr.Fail<object>("update");
                
                var updateSubProducts = await _db.GetAllAsync<LeadSubProduct>(w => w.FK_LeadID == data.FK_LeadID);
                var check = dto.SubProducts.Where(s => s.Selected).ToList();
                foreach (var subProduct in updateSubProducts)
                {
                    if (!check.Where(s => s.SubProductId == subProduct.ID).Any())
                    {
                        subProduct.IsApproved = false;
                        if(!await _db.UpdateAsync<LeadSubProduct>(subProduct))
                        {
                            await tran.RollbackAsync();
                            return Rr.Fail<object>("Create");
                        }
                    }
                }
            }
            else
            {
                if (!await _db.PostAsync(map))
                {
                    await tran.RollbackAsync();
                    return Rr.Fail<object>("Create");
                }
                var checkParent = new LeadSubProduct(); 
                //Saving Selected Products 
                foreach (var subProduct in dto.SubProducts.Where(s => s.Selected).ToList())
                {
                    if (!await _db.IsAnyAsync<SubProduct>(w => w.ID == subProduct.SubProductId))
                        return Rr.NotFound<object>("Product", subProduct.ToString());

                    checkParent = await _db.GetAsync<LeadSubProduct>(w => w.FK_LeadID == dto.Fk_LeadId && w.FK_SubProductId == subProduct.SubProductId);
                    if (checkParent is not null)
                    {
                        checkParent.IsApproved = true;
                        if(!await _db.UpdateAsync(checkParent))
                        {
                            await tran.RollbackAsync();
                            return Rr.Fail<object>("update parent");
                        }
                    }
                    else
                          if (!await _db.PostAsync<LeadSubProduct>(new()
                          {
                              FK_LeadID = dto.Fk_LeadId,
                              FK_CreatedBy = createdBy,
                              FK_SubProductId = subProduct.SubProductId,
                              FK_StagetId = 3,
                              StageCount = 3,
                              IsApproved = true
                        }))
                            {
                                await tran.RollbackAsync();
                                return Rr.Fail<object>("Create");
                            }



                }
            }

            var status = new LeadStatus()
            {
                FK_CreatedBy = createdBy,
                FK_LeadId = dto.Fk_LeadId,
                FK_StatusId = await UpdateSnsStatus(dto.Fk_StatusId ?? 0),
            };
          
            //SAving Status For Stage 3
            if (!await _db.PostAsync<LeadStatus>(status))
            {
                await tran.RollbackAsync();
                return Rr.Fail<object>("Create");
            }



            await tran.CommitAsync();
            return Rr.Success<object>("Created", map.ID);
        }

        public async Task<Responder<List<GetSnsPendingAndByPassListDto>>> GetAllSnsAsync()
        {
            var data = await _db.GetAllByAsync<Lead, GetSnsPendingAndByPassListDto>(SnsMapper.GetSnsPendingAndSnsByPass, w =>
            w.Fk_StatusId != 4 && (w.Fk_StatusId == 2  || w.SNS.FK_StatusID == 13),
                    orderBy: o => o.ID,
                    IsAsending: false
            );
            return Rr.SuccessFetch(data);
        }

        public async Task<Responder<List<GetSnsPendingAndByPassListDto>>> GetAllSnsFailAsync()
        {
            var data = await _db.GetAllByAsync<Lead, GetSnsPendingAndByPassListDto>(SnsMapper.GetSnsPendingAndSnsByPass, w =>
            w.Fk_StatusId != 4 && w.Fk_StatusId == 19,
                    orderBy: o => o.ID,
                    IsAsending: false
            );
            return Rr.SuccessFetch(data);
        }


        public async  Task<Responder<GetSnsDetailDto>> GetSnsByLeadIdAsync(int leadId)
        {
            var data = await _db.GetByAsync<Lead, GetSnsDetailDto>(w => w.ID == leadId,SnsMapper.GetDetails);
            foreach (var item in data.Products.DistinctBy(d => d.Id).ToList())
            {
                /*  data.SubProducts.AddRange( 
                      await _db.GetAllByAsync<SubProduct, GetSubProductsInLeadDto>(SnsMapper.GetSubProducts,
                      w => w.FK_ProductId == item.Id && !w.IsParent
                      ));   */
                data.SubProductsOfProduct.AddRange(
                    await _db.GetAllByAsync<Product, GetSubProductOfProductsDto>(SnsMapper.GetSubProductsOfProduct,
                    w => w.ID == item.Id 
                    ));
            }
            data.Products = null;
            return Rr.SuccessFetch(data);
        }

        public Task<Responder<object>> UpdateSnsAsync(UpdateSnsDto dto)
        {
            throw new NotImplementedException();
        }

        private async Task<int> UpdateSnsStatus(int status)
        {
            if (status == 24)
                return 24;
            else
                 return status == 12 ? 19 : 3;
        }
    }
}
