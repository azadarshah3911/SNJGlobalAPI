using AutoMapper;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DbModelsProduction;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.GeneralServices
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Product
                CreateMap<AddProductDto, Product>();
            #endregion

            #region Patient
                CreateMap<AddPatientDto, Patient>();
            CreateMap<Patient, PatientLogs>();
                CreateMap<UpdatePatientDto, Patient>();
            #endregion

            #region Lead
            CreateMap<AddLeadDto, Patient>();
            #endregion

            #region Eligibility
            CreateMap<AddEligibilityDto, Eligibility>();
            CreateMap<EditEligibilityDto, Eligibility>();
            #endregion

            #region Lead Comments
            CreateMap<AddLeadCommentDto, LeadComments>();
            #endregion

            #region SNS
            CreateMap<AddSnsDto, SNS>();
            #endregion

            #region QA
            CreateMap<AddQaDto, QA>();
            #endregion
            #region Lead Assigned
            CreateMap<AddLeadAssignedDto, LeadAssigned>();
            #endregion
            #region Agent Binus
            CreateMap<AddAgentBonusDto, UserBonus>();
            #endregion
            #region Chassing
            CreateMap<AddChassingDto, Chassing>();
            CreateMap<AddChassingVerificationDto, ChassingVerification>();
            #endregion
            #region Chassing
            CreateMap<AddConfiramtionDto, Confirmation>();
            #endregion
            CreateMap<SignupInListDto, User>();

        }
    }


    public static class Map
    {

        #region Product
        public static MapperConfiguration MapProductDd() =>
             new(cfg => cfg.CreateProjection<Product, ProductionDdDto>());

        public static MapperConfiguration MapProductGet() =>
          new(
                     cfg => cfg.CreateProjection<Product, GetProductDto>()
                     .ForMember(f => f.CreatedBy, x => x.MapFrom(d => d.CreatedBy.FirstName + " " + d.CreatedBy.LastName))
                     /*.ForMember(f => f.UpdatedBy, x => x.MapFrom(d => String.IsNullOrEmpty(d.updatedBy.FirstName) ? null : d.updatedBy.FirstName + " " + d.updatedBy.LastName))*/
                 );
        #endregion

        #region Patient
      /*  public static MapperConfiguration MapPatientDd() =>
             new(cfg => cfg.CreateProjection<Patient, PatientDdDto>());
*/
        public static MapperConfiguration MapPatientGet() =>
          new(
                     cfg => cfg.CreateProjection<Patient, GetPatientDto>()
                     .ForMember(f => f.State, x => x.MapFrom(d => d.State.Code + " - "+ d.State.Name))
                     .ForMember(f => f.FK_CreatedBy, x => x.MapFrom(d => d.CreatedBy.FirstName + " " + d.CreatedBy.LastName))
                     .ForMember(f => f.FK_UpdatedBy, x => x.MapFrom(d => d.UpdatedBy.FirstName + " " + d.CreatedBy.LastName))
                    );

        public static MapperConfiguration MapPatientLeadGet() =>
       new(
                  cfg => cfg.CreateProjection<Patient, PatientLeadListDto>()
                  .ForMember(f => f.State, x => x.MapFrom(d => d.State.Code + " - " + d.State.Name))
                  .ForMember(f => f.LeadCount, x => x.MapFrom(d => d.Leads.Count()))
                 );

        public static MapperConfiguration MapPatientForAgentGet() =>
         new(
                    cfg => cfg.CreateProjection<Patient, GetPatientFoAgentDto>()
                    .ForMember(f => f.ID, x => x.MapFrom(d => d.ID))
                    .ForMember(f => f.MedicareID, x => x.MapFrom(d => d.MedicareID))
                   );

        public static MapperConfiguration MapPatientForAgentByssnGet() =>
        new(
                   cfg => cfg.CreateProjection<Patient, GetPatientForAgenBySsntDto>()
                   .ForMember(f => f.ID, x => x.MapFrom(d => d.ID))
                   .ForMember(f => f.Ssn, x => x.MapFrom(d => d.Ssn))
                  );

        public static MapperConfiguration CheckPatientSameProduct() =>
    new MapperConfiguration(cfg => cfg.CreateProjection<Patient, CheckPatientSameProductDto>()
        .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src =>
            src.Leads.SelectMany(lead => lead.LeadSubProducts)
                     .Select(subProduct => subProduct.FK_SubProductId.GetValueOrDefault())  // Convert to int
                     .ToList()
        ))
    );



        public static MapperConfiguration StateDd() =>
         new(
                    cfg => cfg.CreateProjection<State, GetStateDdDto>()
                    );
        public static MapperConfiguration BranchDd() =>
        new(
                   cfg => cfg.CreateProjection<Branch, GetBranchDdDto>()
                   );
        public static MapperConfiguration AgenthDd() =>
      new(
                 cfg =>
                 {
                     cfg.CreateProjection<User, GetAgentDdDto>()
                   .ForMember(f => f.ID, x => x.MapFrom(d => d.ID))
                   .ForMember(f => f.Name, x => x.MapFrom(d => d.FirstName + " " + d.LastName))
                   .ForMember(f => f.Roles, x => x.MapFrom(d => d.usersRoles));
                     
                    cfg.CreateProjection<UserRole, GetDdRolesForUserDto>()
                     .ForMember(a => a.RoleName, o => o.MapFrom(p => p.role.Name))
                     .ForMember(a => a.RoleId, o => o.MapFrom(p => p.role.Id));
                 });
        #endregion
    }
}
