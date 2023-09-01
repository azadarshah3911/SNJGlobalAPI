using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DbModelsProduction;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Mappers
{
    public class QaMapper
    {
        //For Superadmin
        public static MapperConfiguration GetQAList = new
        (
            c =>
            {
                //List For Lead And Patient Infoemation
                c.CreateProjection<Lead, GetQAListDto>()
                .ForMember(a => a.Patient, o => o.MapFrom(p => p.Patient))
                .ForMember(a => a.Lead, o => o.MapFrom(p => p))
                .ForMember(a => a.EvStatus, o => o.MapFrom(p => p.Eligibilities.OrderByDescending(o => o.ID).FirstOrDefault().Status.Name))
                .ForMember(a => a.SnsStatus, o => o.MapFrom(p => p.SNS.Status.Name))
                .ForMember(a => a.QaStatus, o => o.MapFrom(p => p.QA.OrderByDescending(o => o.ID).FirstOrDefault().Status.Name))
                .ForMember(a => a.ProductName, o => o.MapFrom(p => p.LeadSubProducts.OrderByDescending(o => o.ID).FirstOrDefault().SubProduct.Product.Name))
                .ForMember(a => a.Assigned, o => o.MapFrom(p => p.LeadAssigneds.Where(w => w.FK_StageId == 4).OrderByDescending(d => d.Id).FirstOrDefault()))
                .ForMember(a => a.ChassingStatus, o => o.MapFrom(p => p.Chassings.OrderByDescending(d => d.Id).FirstOrDefault().Status.Name));

                c.CreateProjection<Patient, GetPatientForSnsDto>()
                .ForMember(a => a.StateCode, o => o.MapFrom(m => m.State.Code))
                .ForMember(a => a.State, o => o.MapFrom(m => m.State.Name));

                c.CreateProjection<Lead, GetLeadForSnsDto>()
                .ForMember(a => a.AgentName, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName))
                .ForMember(a => a.AgentBranch, o => o.MapFrom(m => m.CreatedBy.branch.Name))
                .ForMember(a => a.AgentId, o => o.MapFrom(m => m.CreatedBy.ID))
                .ForMember(a => a.LeadId, o => o.MapFrom(m => m.ID))
                .ForMember(a => a.ProductName, o => o.MapFrom(m => m.LeadSubProducts.OrderByDescending(o => o.ID).FirstOrDefault().SubProduct.Product.Name))
                .ForMember(a => a.LeadStatus, o => o.MapFrom(m => m.Status.Name));

                c.CreateProjection<LeadAssigned, CheckQaAssignedLeads>()
                .ForMember(a => a.SuperVisor, o => o.MapFrom(m => m.Supervisor.FirstName + " " + m.Supervisor.LastName))
                .ForMember(a => a.Agent, o => o.MapFrom(m => m.Agent.FirstName + " " + m.Agent.LastName));
            });

        //For Agents
        public static MapperConfiguration GetQAListForAgent = new
       (
           c =>
           {
                //List For Lead And Patient Infoemation
                c.CreateProjection<LeadAssigned, GetQAListForAgentDto>()
                .ForMember(a => a.Patient, o => o.MapFrom(p => p.Lead.Patient))
                .ForMember(a => a.Lead, o => o.MapFrom(p => p.Lead))
                .ForMember(a => a.EvStatus, o => o.MapFrom(p => p.Lead.Eligibilities.OrderByDescending(o => o.ID).FirstOrDefault().Status.Name))
                .ForMember(a => a.SnsStatus, o => o.MapFrom(p => p.Lead.SNS.Status.Name))
                .ForMember(a => a.QaStatus, o => o.MapFrom(p => p.Lead.QA.OrderByDescending(o => o.ID).FirstOrDefault().Status.Name))
                .ForMember(a => a.ChassingStatus, o => o.MapFrom(p => p.Lead.Chassings.OrderByDescending(d => d.Id).FirstOrDefault().Status.Name));

               c.CreateProjection<Patient, GetPatientForSnsDto>()
               .ForMember(a => a.StateCode, o => o.MapFrom(m => m.State.Code))
               .ForMember(a => a.State, o => o.MapFrom(m => m.State.Name));

               c.CreateProjection<Lead, GetLeadForSnsDto>()
               .ForMember(a => a.AgentName, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName))
               .ForMember(a => a.AgentBranch, o => o.MapFrom(m => m.CreatedBy.branch.Name))
               .ForMember(a => a.AgentId, o => o.MapFrom(m => m.CreatedBy.ID))
               .ForMember(a => a.LeadId, o => o.MapFrom(m => m.ID))
                .ForMember(a => a.ProductName, o => o.MapFrom(m => m.LeadSubProducts.OrderByDescending(o => o.ID).FirstOrDefault().SubProduct.Product.Name))
               .ForMember(a => a.LeadStatus, o => o.MapFrom(m => m.Status.Name));
           });

        public static MapperConfiguration GetDetails = new
        (
            c =>
            {
                //List For Lead And Patient Infoemation
                c.CreateProjection<Lead, GetQaDetailDto>()
                .ForMember(a => a.Patient, o => o.MapFrom(p => p.Patient))
                .ForMember(a => a.Lead, o => o.MapFrom(p => p))
                .ForMember(a => a.Eligibilities, o => o.MapFrom(p => p.Eligibilities.OrderByDescending(o => o.ID)))
                .ForMember(a => a.SubProducts, o => o.MapFrom(p => p.LeadSubProducts.Where(w => w.IsApproved)))
                .ForMember(a => a.Sns, o => o.MapFrom(p => p.SNS))
                .ForMember(a => a.Qas, o => o.MapFrom(p => p.QA.OrderByDescending(order => order.ID)))
                .ForMember(a => a.FirstStageQuesAns, o => o.MapFrom(p => p.ProductQuestionAnswer))
                .ForMember(a => a.Chassing, o => o.MapFrom(p => p.Chassings.OrderByDescending(order => order.Id)));

                //Patient
                c.CreateProjection<Patient, GetPatientForSnsDto>()
                .ForMember(a => a.StateCode, o => o.MapFrom(m => m.State.Code))
                .ForMember(a => a.State, o => o.MapFrom(m => m.State.Name));

                //Lead
                c.CreateProjection<Lead, GetLeadForSnsDto>()
                .ForMember(a => a.AgentName, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName))
                .ForMember(a => a.AgentBranch, o => o.MapFrom(m => m.CreatedBy.branch.Name))
                .ForMember(a => a.AgentId, o => o.MapFrom(m => m.CreatedBy.ID))
                .ForMember(a => a.LeadId, o => o.MapFrom(m => m.ID))
                .ForMember(a => a.LeadStatus, o => o.MapFrom(m => m.Status.Name))
                .ForMember(a => a.ProductName, o => o.MapFrom(m => m.LeadSubProducts.FirstOrDefault().SubProduct.Product.Name));

                //Eligibilities
                c.CreateProjection<Eligibility, GetEligibilityForDetailsDto>()
                .ForMember(a => a.Status, o => o.MapFrom(m => m.Status.Name))
                .ForMember(a => a.CreatedBy, o => o.MapFrom(m => m.User.FirstName + " " + m.User.LastName));

                //SNS
                c.CreateProjection<SNS, GetSnsForQaDto>()
                .ForMember(a => a.Status, o => o.MapFrom(m => m.Status.Name))
                .ForMember(a => a.CreatedBy, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName));

                //Sub Product
                c.CreateProjection<LeadSubProduct, GetSubProductsInLeadForQaDto>()
                .ForMember(a => a.ID, o => o.MapFrom(m => m.SubProduct.ID))
                .ForMember(a => a.Code, o => o.MapFrom(m => m.SubProduct.Code))
                .ForMember(a => a.Name, o => o.MapFrom(m => m.SubProduct.Name))
                .ForMember(a => a.ProductName, o => o.MapFrom(m => m.SubProduct.Product.Name));

                //QA List
                c.CreateProjection<QA, GetQAForLeadDto>()
                .ForMember(a => a.Status, o => o.MapFrom(m => m.Status.Name))
                .ForMember(a => a.CreatedBy, o => o.MapFrom(m => m.CreatedBy.FirstName +" "+ m.CreatedBy.LastName))
                .ForMember(a => a.Answers, o => o.MapFrom(m => m.Answers));

                //Question Answer List
                c.CreateProjection<QaQuestionAnswer, GetQaAnswerForLeadDto>()
                .ForMember(a => a.QuestionId, o => o.MapFrom(m => m.Question.ID))
                .ForMember(a => a.Question, o => o.MapFrom(m => m.Question.Question))
                .ForMember(a => a.ProductName, o => o.MapFrom(m => m.Question.Product.Name));

                //For Question Answer
                c.CreateProjection<ProductQuestionAnswer, GetLeadQuestionAnsDto>()
                .ForMember(a => a.QuestionId, o => o.MapFrom(p => p.ProductQuestion.ID))
                .ForMember(a => a.AnswerId, o => o.MapFrom(p => p.ID))
                .ForMember(a => a.Question, o => o.MapFrom(p => p.ProductQuestion.Question))
                .ForMember(a => a.ProductName, o => o.MapFrom(p => p.ProductQuestion.Product.Name));

                //Chassing List
                c.CreateProjection<Chassing, GetProcessedForExcelDto>()
                .ForMember(a => a.Status, o => o.MapFrom(m => m.Status.Name))
                .ForMember(a => a.Fk_StatusId, o => o.MapFrom(m => m.Status.ID))
                .ForMember(a => a.CreatedBy, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName));

            });


        public static MapperConfiguration GetLeadProductId = new
       (
           c =>
           {
               //List For Lead And Patient Infoemation
               c.CreateProjection<LeadSubProduct, GetSelectedProductDto>()
               .ForMember(a => a.ID, o => o.MapFrom(p => p.SubProduct.Product.ID));

              // c.CreateProjection<LeadSubProduct, GetSelectedProductDto>()
              //.ForMember(a => a.SelectedProducts, o => o.MapFrom(p => p.SubProduct.Product.ID));

           }
        );

        public static MapperConfiguration GetQuestionsForQa = new
      (
          c =>
          {
              //List For Lead And Patient Infoemation
              c.CreateProjection<ProductQuestion, GetProductQuestionForQaDto>()
              .ForMember(a => a.ProductName ,o => o.MapFrom(p => p.Product.Name));

          }
       );
    }
}
