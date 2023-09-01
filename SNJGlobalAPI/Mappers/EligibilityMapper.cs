
using AutoMapper;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DbModelsProduction;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Mappers
{
    public class EligibilityMapper
    {
        public static MapperConfiguration GetNewLeadList = new 
        (s => {
            //List For Lead And Patient Infoemation
            s.CreateProjection<Lead, GetNewLeadListDto>()
            .ForMember(a => a.MedicareID, o => o.MapFrom(p => p.Patient.MedicareID))
            .ForMember(a => a.Ssn, o => o.MapFrom(p => p.Patient.Ssn))
            .ForMember(a => a.FirstName, o => o.MapFrom(p => p.Patient.FirstName))
            .ForMember(a => a.MiddleName, o => o.MapFrom(p => p.Patient.MiddleName))
            .ForMember(a => a.Ssn, o => o.MapFrom(p => p.Patient.Ssn))
            .ForMember(a => a.LastName, o => o.MapFrom(p => p.Patient.LastName))
            .ForMember(a => a.Suffix, o => o.MapFrom(p => p.Patient.Suffix))
            .ForMember(a => a.PhoneNumber, o => o.MapFrom(p => p.Patient.PhoneNumber))
            .ForMember(a => a.DateofBirth, o => o.MapFrom(p => p.Patient.DateofBirth))
            .ForMember(a => a.Address, o => o.MapFrom(p => p.Patient.Address))
            .ForMember(a => a.Address2, o => o.MapFrom(p => p.Patient.Address2))
            .ForMember(a => a.City, o => o.MapFrom(p => p.Patient.City))
            .ForMember(a => a.ZipCode, o => o.MapFrom(p => p.Patient.ZipCode))
            .ForMember(a => a.Gender, o => o.MapFrom(p => p.Patient.Gender))
            .ForMember(a => a.ID, o => o.MapFrom(p => p.Patient.ID))
            .ForMember(a => a.LeadId, o => o.MapFrom(p => p.ID))
            .ForMember(a => a.State, o => o.MapFrom(p => p.Patient.State.Name))
            .ForMember(a => a.AgentName, o => o.MapFrom(p => p.CreatedBy.FirstName + " " + p.CreatedBy.LastName))
            .ForMember(a => a.AgentBranch, o => o.MapFrom(p => p.CreatedBy.branch.Name))
            .ForMember(a => a.AgentId, o => o.MapFrom(p => p.CreatedBy.ID))
            .ForMember(a => a.EvStatus, o => o.MapFrom(p => p.Eligibilities.OrderByDescending(d => d.ID).FirstOrDefault().Status.Name))
            .ForMember(a => a.ProductName, o => o.MapFrom(p => p.LeadSubProducts.FirstOrDefault().SubProduct.Product.Name))
            .ForMember(a => a.Status, o => o.MapFrom(p => p.leadStatuses.Where(w => w.FK_StatusId == 1 || w.FK_StatusId == 4).OrderByDescending(m => m.ID).FirstOrDefault().status.Name));
        });

        public static MapperConfiguration GetNewLeadDetails = new
        (s => {
            //Details For Lead And Patient Information
            s.CreateProjection<Lead, GetNewLeadDetailsDto>()
            .ForMember(a => a.PatientID, o => o.MapFrom(p => p.Patient.ID))
            .ForMember(a => a.MedicareID, o => o.MapFrom(p => p.Patient.MedicareID))
            .ForMember(a => a.Ssn, o => o.MapFrom(p => p.Patient.Ssn))
            .ForMember(a => a.FirstName, o => o.MapFrom(p => p.Patient.FirstName))
            .ForMember(a => a.MiddleName, o => o.MapFrom(p => p.Patient.MiddleName))
            .ForMember(a => a.LastName, o => o.MapFrom(p => p.Patient.LastName))
            .ForMember(a => a.Suffix, o => o.MapFrom(p => p.Patient.Suffix))
            .ForMember(a => a.PhoneNumber, o => o.MapFrom(p => p.Patient.PhoneNumber))
            .ForMember(a => a.DateofBirth, o => o.MapFrom(p => p.Patient.DateofBirth))
            .ForMember(a => a.Address, o => o.MapFrom(p => p.Patient.Address))
            .ForMember(a => a.Address2, o => o.MapFrom(p => p.Patient.Address2))
            .ForMember(a => a.City, o => o.MapFrom(p => p.Patient.City))
            .ForMember(a => a.ZipCode, o => o.MapFrom(p => p.Patient.ZipCode))
            .ForMember(a => a.Gender, o => o.MapFrom(p => p.Patient.Gender))
            .ForMember(a => a.ID, o => o.MapFrom(p => p.Patient.ID))
            .ForMember(a => a.LeadId, o => o.MapFrom(p => p.ID))
            .ForMember(a => a.State, o => o.MapFrom(p => p.Patient.State.Name))
            .ForMember(a => a.StateId, o => o.MapFrom(p => p.Patient.State.ID))
            .ForMember(a => a.AgentName, o => o.MapFrom(p => p.CreatedBy.FirstName + " " + p.CreatedBy.LastName))
            .ForMember(a => a.AgentBranch, o => o.MapFrom(p => p.CreatedBy.branch.Name))
            .ForMember(a => a.AgentId, o => o.MapFrom(p => p.CreatedBy.ID))
            .ForMember(a => a.Products, o => o.MapFrom(p => p.LeadSubProducts))
            .ForMember(a => a.QuesAns, o => o.MapFrom(p => p.ProductQuestionAnswer))
            .ForMember(a => a.LeadStatus, o => o.MapFrom(p => p.leadStatuses))
            .ForMember(a => a.Notes, o => o.MapFrom(p => p.Notes))

            .ForMember(a => a.Eligibilities, o => o.MapFrom(p => p.Eligibilities))
            .ForMember(a => a.Penalties, o => o.MapFrom(p => p.AgentPenalties.Where(w => w.Fk_StageId == 2)))
            .ForMember(a => a.Files, o => o.MapFrom(p => p.LeadFiles.Where(w => w.FK_StageId == 2)));
            //.ForMember(a => a.LeadComments, o => o.MapFrom(p => p.LeadComments.Where(w => w.Fk_StageId == 2)));

            //For Produt Name
            s.CreateProjection<LeadSubProduct, GetLeadProductQuestionsDto>()
            .ForMember(a => a.ProductName, o => o.MapFrom(p => p.SubProduct.Product.Name));

            //For Question Answer
            s.CreateProjection<ProductQuestionAnswer, GetLeadQuestionAnsDto>()
            .ForMember(a => a.QuestionId, o => o.MapFrom(p => p.ProductQuestion.ID))
            .ForMember(a => a.AnswerId, o => o.MapFrom(p => p.ID))
            .ForMember(a => a.Question, o => o.MapFrom(p => p.ProductQuestion.Question))
            .ForMember(a => a.ProductName, o => o.MapFrom(p => p.ProductQuestion.Product.Name));
            //Lead Status
            s.CreateProjection<LeadStatus, GetLeadStatusDto>()
            .ForMember(m => m.LeadStatus, l => l.MapFrom(k => k.status.Name))
            .ForMember(m => m.CreatedAt, l => l.MapFrom(k => k.CreatedAt))
            .ForMember(m => m.AgentName, l => l.MapFrom(k => k.createdBy.FirstName + " " + k.createdBy.LastName));

            //lead eligibilities
            s.CreateProjection<Eligibility, GetEligibilityForDetailsDto>()
            .ForMember(m => m.Status, l => l.MapFrom(k => k.Status.Name))
            .ForMember(m => m.ElgibilityRemarks, l => l.MapFrom(k => k.ElgibilityRemarks))
            .ForMember(m => m.CreatedBy, l => l.MapFrom(k => k.User.FirstName + " " + k.User.LastName));

            //lead penalities
            s.CreateProjection<AgentPenalty, GetPenaltyForDetailsDto>()
            .ForMember(m => m.PenaltyTo, l => l.MapFrom(k => k.PenaltyTo.FirstName + " " + k.PenaltyTo.LastName))
            .ForMember(m => m.PenaltyFrom, l => l.MapFrom(k => k.PenaltyFrom.FirstName + " " + k.PenaltyFrom.LastName));

            //lead files
            s.CreateProjection<LeadFile, GetLeadFileForDetails>();

            //lead Comments
            //s.CreateProjection<LeadComments, GetLeadCommentDto>()
            //.ForMember(m => m.CreatedBy, l => l.MapFrom(k => k.User.FirstName+ " " + k.User.LastName));
        });
    }
}
