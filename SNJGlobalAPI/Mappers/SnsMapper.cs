using AutoMapper;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DbModelsProduction;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Mappers
{
    public class SnsMapper
    {
        public static MapperConfiguration GetSnsPendingAndSnsByPass = new
        (
            c =>
            {
                //List For Lead And Patient Infoemation
                c.CreateProjection<Lead, GetSnsPendingAndByPassListDto>()
                .ForMember(a => a.Patient, o => o.MapFrom(p => p.Patient))
                .ForMember(a => a.Lead, o => o.MapFrom(p => p))
                .ForMember(a => a.EvStatus, o => o.MapFrom(p => p.Eligibilities.OrderByDescending(order => order.ID).FirstOrDefault().Status.Name))
                .ForMember(a => a.SnsStatus, o => o.MapFrom(p => p.SNS.Status.Name));

                c.CreateProjection<Patient, GetPatientForSnsDto>()
                .ForMember(a => a.StateCode, o => o.MapFrom(m => m.State.Code))
                .ForMember(a => a.State, o => o.MapFrom(m => m.State.Name))
                .ForMember(a => a.Fk_StateId, o => o.MapFrom(m => m.FK_StateId));

                c.CreateProjection<Lead, GetLeadForSnsDto>()
                .ForMember(a => a.AgentName, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName))
                .ForMember(a => a.AgentBranch, o => o.MapFrom(m => m.CreatedBy.branch.Name))
                .ForMember(a => a.AgentId, o => o.MapFrom(m => m.CreatedBy.ID))
                .ForMember(a => a.LeadId, o => o.MapFrom(m => m.ID))
                .ForMember(a => a.LeadStatus, o => o.MapFrom(m => m.Status.Name))
                .ForMember(a => a.ProductName, o => o.MapFrom(m => m.LeadSubProducts.FirstOrDefault().SubProduct.Product.Name));
            });


        public static MapperConfiguration GetDetails = new
        (
            c =>
            {
                //List For Lead And Patient Infoemation
                c.CreateProjection<Lead, GetSnsDetailDto>()
                .ForMember(a => a.Patient, o => o.MapFrom(p => p.Patient))
                .ForMember(a => a.Lead, o => o.MapFrom(p => p))
                .ForMember(a => a.Eligibilities, o => o.MapFrom(p => p.Eligibilities.OrderByDescending(o => o.ID)))
                .ForMember(a => a.Products, o => o.MapFrom(p => p.LeadSubProducts))
                .ForMember(a => a.Sns, o => o.MapFrom(p => p.SNS))
                .ForMember(a => a.SnsStatus, o => o.MapFrom(p => p.SNS.Status.Name))
                .ForMember(a => a.QuesAns, o => o.MapFrom(p => p.ProductQuestionAnswer));

                c.CreateProjection<Patient, GetPatientForSnsDto>()
                .ForMember(a => a.StateCode, o => o.MapFrom(m => m.State.Code))
                .ForMember(a => a.State, o => o.MapFrom(m => m.State.Name));

                c.CreateProjection<Lead, GetLeadForSnsDto>()
                .ForMember(a => a.AgentName, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName))
                .ForMember(a => a.AgentBranch, o => o.MapFrom(m => m.CreatedBy.branch.Name))
                .ForMember(a => a.AgentId, o => o.MapFrom(m => m.CreatedBy.ID))
                .ForMember(a => a.LeadId, o => o.MapFrom(m => m.ID))
                .ForMember(a => a.LeadStatus, o => o.MapFrom(m => m.Status.Name))
                .ForMember(a => a.ProductName, o => o.MapFrom(m => m.LeadSubProducts.FirstOrDefault().SubProduct.Product.Name));

                c.CreateProjection<Eligibility, GetEligibilityForDetailsDto>()
                .ForMember(a => a.Status, o => o.MapFrom(m => m.Status.Name))
                .ForMember(a => a.CreatedBy, o => o.MapFrom(m => m.User.FirstName+" "+ m.User.LastName));

                c.CreateProjection<LeadSubProduct, GetProductIdDto>()
                .ForMember(a => a.Id, o => o.MapFrom(m => m.SubProduct.Product.ID));

                //SNS
                c.CreateProjection<SNS, GetSnsForSnsDto>()
                .ForMember(a => a.Id, o => o.MapFrom(m => m.ID))
                .ForMember(a => a.Status, o => o.MapFrom(m => m.Status.Name))
                .ForMember(a => a.CreatedBy, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName));

                //For Question Answer
                c.CreateProjection<ProductQuestionAnswer, GetLeadQuestionAnsDto>()
                .ForMember(a => a.QuestionId, o => o.MapFrom(p => p.ProductQuestion.ID))
                .ForMember(a => a.AnswerId, o => o.MapFrom(p => p.ID))
                .ForMember(a => a.Question, o => o.MapFrom(p => p.ProductQuestion.Question))
                .ForMember(a => a.ProductName, o => o.MapFrom(p => p.ProductQuestion.Product.Name));
            });


        public static MapperConfiguration GetSubProducts = new
       (
           c =>
           {
               //List For Lead And Patient Infoemation
               c.CreateProjection<SubProduct, GetSubProductsInLeadDto>()
               .ForMember(a => a.ProductName, o => o.MapFrom(p => p.Product.Name));

           }
        );

        public static MapperConfiguration GetSubProductsOfProduct = new
              (
                  c =>
                  {
                      //List For Lead And Patient Infoemation
                      c.CreateProjection<Product, GetSubProductOfProductsDto>()
                      .ForMember(a => a.ProductName, o => o.MapFrom(p => p.Name));

                      c.CreateProjection<SubProduct, GetSubProductsDto>()
                        .ForMember(a => a.Id, o => o.MapFrom(p => p.ID))
                        .ForMember(a => a.Name, o => o.MapFrom(p => p.Name))
                        .ForMember(a => a.IsParent, o => o.MapFrom(p => p.IsParent))
                        .ForMember(a => a.Code, o => o.MapFrom(p => p.Code));
                  }
               );

    }
}
