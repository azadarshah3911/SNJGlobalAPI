using AutoMapper;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DbModelsProduction;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Mappers
{
    public class LeadMapper
    {
        public static MapperConfiguration GetLeadDetails = new
       (
           c =>
           {
               //List For Lead And Patient Infoemation
               c.CreateProjection<Lead, GetLeadDetailsDto>()
                   .ForMember(a => a.Patient, o => o.MapFrom(p => p.Patient))
                   .ForMember(a => a.Lead, o => o.MapFrom(p => p))
                   .ForMember(a => a.Eligibilities, o => o.MapFrom(p => p.Eligibilities))
                   .ForMember(a => a.SubProducts, o => o.MapFrom(p => p.LeadSubProducts.Where(w => w.IsApproved)))
                   .ForMember(a => a.Sns, o => o.MapFrom(p => p.SNS))
                   .ForMember(a => a.Qas, o => o.MapFrom(p => p.QA.OrderByDescending(order => order.ID)))
                   .ForMember(a => a.LeadStatuses, o => o.MapFrom(p => p.leadStatuses.OrderByDescending(order => order.ID)))
                   .ForMember(a => a.Penalties, o => o.MapFrom(p => p.AgentPenalties))
                   .ForMember(a => a.LeadFiles, o => o.MapFrom(p => p.LeadFiles.Select(s => s.File)))
                   .ForMember(a => a.QaFiles, o => o.MapFrom(p => p.QA.SelectMany(s => s.Files).Select(s => s.File)))
                   .ForMember(a => a.PatientLogs, o => o.MapFrom(p => p.LeadLogs.OrderByDescending(order => order.ID)))
                   .ForMember(a => a.FirstStageQuesAns, o => o.MapFrom(p => p.ProductQuestionAnswer))
                   .ForMember(a => a.Chassing, o => o.MapFrom(p => p.Chassings.OrderByDescending(order => order.Id)))
                   .ForMember(a => a.ChassingFiles, o => o.MapFrom(p => p.ChassingFiles.SelectMany(s => s.ChassingFiles).Select(s => s.File)));


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
               .ForMember(a => a.Code, o => o.MapFrom(m => m.SubProduct.Code))
               .ForMember(a => a.Name, o => o.MapFrom(m => m.SubProduct.Name))
               .ForMember(a => a.ProductName, o => o.MapFrom(m => m.SubProduct.Product.Name));

               //QA List
               c.CreateProjection<QA, GetQAForLeadDto>()
               .ForMember(a => a.Status, o => o.MapFrom(m => m.Status.Name))
               .ForMember(a => a.CreatedBy, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName))
               .ForMember(a => a.Answers, o => o.MapFrom(m => m.Answers));

               //QA Answer List
               c.CreateProjection<QaQuestionAnswer, GetQaAnswerForLeadDto>()
               .ForMember(a => a.QuestionId, o => o.MapFrom(m => m.Question.ID))
               .ForMember(a => a.Question, o => o.MapFrom(m => m.Question.Question))
               .ForMember(a => a.ProductName, o => o.MapFrom(m => m.Question.Product.Name));

               //Lead Statuses List
               c.CreateProjection<LeadStatus, GetLeadStatusesDto>()
             .ForMember(a => a.CreatedBy, o => o.MapFrom(m => m.createdBy.FirstName + " " + m.createdBy.LastName))
             .ForMember(a => a.Status, o => o.MapFrom(m => m.status.Name));

               //Lead Penalties List
               c.CreateProjection<AgentPenalty, GetAgentPenaltyDto>()
                .ForMember(member => member.LeadId, source => source.MapFrom(map => map.Fk_LeadID))
                .ForMember(member => member.PatientId, source => source.MapFrom(map => map.Lead.Patient.ID))
                .ForMember(member => member.PenaltyFrom, source => source.MapFrom(map => map.PenaltyFrom.FirstName + " " + map.PenaltyFrom.LastName))
                .ForMember(member => member.AgentName, source => source.MapFrom(map => map.PenaltyTo.FirstName + " " + map.PenaltyFrom.LastName))
                .ForMember(member => member.Stage, source => source.MapFrom(map => map.Stage.Name))
                .ForMember(member => member.AgentId, source => source.MapFrom(map => map.PenaltyTo.ID));

               //For Question Answer
               c.CreateProjection<ProductQuestionAnswer, GetLeadQuestionAnsDto>()
               .ForMember(a => a.QuestionId, o => o.MapFrom(p => p.ProductQuestion.ID))
               .ForMember(a => a.AnswerId, o => o.MapFrom(p => p.ID))
               .ForMember(a => a.Question, o => o.MapFrom(p => p.ProductQuestion.Question))
               .ForMember(a => a.ProductName, o => o.MapFrom(p => p.ProductQuestion.Product.Name));

               //Patient Logs List
               c.CreateProjection<PatientLogs, GetPateintLogsDto>()
                 .ForMember(a => a.LogCreatedBy, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName))
                 .ForMember(a => a.LogCreatedAt, o => o.MapFrom(m => m.CreatedAt))
                 .ForMember(a => a.LogStage, o => o.MapFrom(m => m.Stage.Name))
                 .ForMember(a => a.LeadId, o => o.MapFrom(m => m.Fk_LeadId));

               //Patient Logs
               c.CreateProjection<PatientLogs, GetPateintLogsDto>()
               .ForMember(a => a.State, o => o.MapFrom(m => m.State.Name))
               .ForMember(a => a.Fk_StateId, o => o.MapFrom(m => m.State.ID));

               //Chassing List
               c.CreateProjection<Chassing, GetProcessedForExcelDto>()
               .ForMember(a => a.Status, o => o.MapFrom(m => m.Status.Name))
               .ForMember(a => a.Fk_StatusId, o => o.MapFrom(m => m.Status.ID))
               .ForMember(a => a.CreatedBy, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName));

           });


        public static MapperConfiguration GetLeadList = new(
            proj =>
            {
                proj.CreateProjection<Lead, leadListDto>()
                 .ForMember(formember => formember.Id, o => o.MapFrom(map => map.ID))
                 .ForMember(formember => formember.Patient, o => o.MapFrom(map => map.Patient))
                 .ForMember(formember => formember.AgentName, o => o.MapFrom(map => map.CreatedBy.FirstName + " " + map.CreatedBy.LastName))
                 .ForMember(formember => formember.AgentBranch, o => o.MapFrom(map => map.CreatedBy.branch.Name))
                 .ForMember(formember => formember.LeadStatus, o => o.MapFrom(map => map.Status.Name))
                 .ForMember(formember => formember.EvStatus, o => o.MapFrom(map => map.Eligibilities.OrderByDescending(order => order.ID).FirstOrDefault().Status.Name))
                 .ForMember(formember => formember.SnsStatus, o => o.MapFrom(map => map.SNS.Status.Name))
                 .ForMember(formember => formember.QaStatus, o => o.MapFrom(map => map.QA.OrderByDescending(order => order.ID).FirstOrDefault().Status.Name))
                 .ForMember(formember => formember.ChassingStatus, o => o.MapFrom(map => map.Chassings.OrderByDescending(order => order.Id).FirstOrDefault().Status.Name))
                 .ForMember(formember => formember.ProductName, o => o.MapFrom(map => map.LeadSubProducts.FirstOrDefault().SubProduct.Product.Name))
                 .ForMember(formember => formember.ChassingVerificationStatus, o => o.MapFrom(map => map.ChassingVerification.Status.Name))
                 .ForMember(formember => formember.Confirmation, o => o.MapFrom(map => map.Confirmation.Status.Name));

                //Patient
                proj.CreateProjection<Patient, GetPatientForSnsDto>()   
                .ForMember(a => a.StateCode, o => o.MapFrom(m => m.State.Code))
                .ForMember(formember => formember.State, o => o.MapFrom(map => map.State.Name));
;


            });

        public static MapperConfiguration GetEditPatient = new(
          proj =>
          {
              proj.CreateProjection<Lead, GetEditPatientForAgentDto>()
               .ForMember(formember => formember.Lead, o => o.MapFrom(map => map))
                   .ForMember(a => a.FirstStageQuesAns, o => o.MapFrom(p => p.ProductQuestionAnswer))
               .ForMember(formember => formember.Patient, o => o.MapFrom(map => map.Patient));
              //Patient
              proj.CreateProjection<Patient, GetPatientForSnsDto>()
              .ForMember(a => a.StateCode, o => o.MapFrom(m => m.State.Code))
              .ForMember(formember => formember.State, o => o.MapFrom(map => map.State.Name));

              //Lead
              proj.CreateProjection<Lead, GetLeadForSnsDto>()
              .ForMember(a => a.AgentName, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName))
              .ForMember(a => a.AgentBranch, o => o.MapFrom(m => m.CreatedBy.branch.Name))
              .ForMember(a => a.AgentId, o => o.MapFrom(m => m.CreatedBy.ID))
              .ForMember(a => a.LeadId, o => o.MapFrom(m => m.ID))
              .ForMember(a => a.LeadStatus, o => o.MapFrom(m => m.Status.Name))
                .ForMember(a => a.ProductName, o => o.MapFrom(m => m.LeadSubProducts.FirstOrDefault().SubProduct.Product.Name));

              //For Question Answer
              proj.CreateProjection<ProductQuestionAnswer, GetLeadQuestionAnsDto>()
              .ForMember(a => a.QuestionId, o => o.MapFrom(p => p.ProductQuestion.ID))
              .ForMember(a => a.AnswerId, o => o.MapFrom(p => p.ID))
              .ForMember(a => a.Question, o => o.MapFrom(p => p.ProductQuestion.Question))
              .ForMember(a => a.ProductName, o => o.MapFrom(p => p.ProductQuestion.Product.Name));


          });
    }
}
