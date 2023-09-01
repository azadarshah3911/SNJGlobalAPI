using AutoMapper;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Mappers
{
    public class ErrorLeadMapper
    {
        public static MapperConfiguration GetErrorList = new
       (
           c =>
           {
               //List For Lead And Patient Infoemation
               c.CreateProjection<Lead, GetErrorLeadListDto>()
               .ForMember(a => a.Patient, o => o.MapFrom(p => p.Patient))
               .ForMember(a => a.Lead, o => o.MapFrom(p => p))
               .ForMember(a => a.EvStatus, o => o.MapFrom(p => p.Eligibilities.OrderByDescending(o => o.ID).FirstOrDefault().Status.Name))
               .ForMember(a => a.SnsStatus, o => o.MapFrom(p => p.SNS.Status.Name))
               .ForMember(a => a.QaStatus, o => o.MapFrom(p => p.QA.OrderByDescending(o => o.ID).FirstOrDefault().Status.Name))
               .ForMember(a => a.StageId, o => o.MapFrom(p => p.Status.Stage.ID))
               .ForMember(a => a.Stage, o => o.MapFrom(p => p.Status.Stage.Name));

               c.CreateProjection<Patient, GetPatientForSnsDto>()
                .ForMember(a => a.StateCode, o => o.MapFrom(m => m.State.Code))
               .ForMember(a => a.State, o => o.MapFrom(m => m.State.Name));

               c.CreateProjection<Lead, GetLeadForSnsDto>()
               .ForMember(a => a.AgentName, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName))
               .ForMember(a => a.AgentBranch, o => o.MapFrom(m => m.CreatedBy.branch.Name))
               .ForMember(a => a.AgentId, o => o.MapFrom(m => m.CreatedBy.ID))
               .ForMember(a => a.LeadId, o => o.MapFrom(m => m.ID))
               .ForMember(a => a.LeadStatus, o => o.MapFrom(m => m.Status.Name));
           });
    }
}
