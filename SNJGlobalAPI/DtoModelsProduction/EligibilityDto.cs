using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using SNJGlobalAPI.DbModelsProduction;

namespace SNJGlobalAPI.DtoModelsProduction
{
    public class AddEligibilityDto
    {
        [Required, StringLength(100), Display(Name = "Primary Insurance")]
        public string PrimaryInsurance { get; set; }

        [Required, Display(Name = "Eligibility Status")]
        public int? FK_StatusId { get; set; }

        [StringLength(275), Display(Name = "HCPS Code")]
        public string HCPSCode { get; set; }

        [StringLength(575), Display(Name = "EV Remarks")]
        public string ElgibilityRemarks { get; set; }
        
        public IFormFile EligibilityFile { get; set; }

        [Required]
        public int FK_LeadID { get; set; }

        public int? AgentId { get; set; }

        //Discuss On Penalty
        public int? Penalty { get; set; }
        public string? PenaltyReason { get; set; }
    }

    public class GetNewLeadListDto
    {
        public int ID { get; set; }
        public string RefrenceCode { get => "SJ" + ID.ToString().PadLeft(4, '0'); }
        public string MedicareID { get; set; }
        public string _MedicareId
        {
            get
            {
                string fullMedicareId = MedicareID.ToString();
                if (fullMedicareId.Length >= 7)
                {
                    string hiddenPart = new string('*', fullMedicareId.Length - 4);
                    string visibleLastCharacter = fullMedicareId.Substring(fullMedicareId.Length - 4);
                    return hiddenPart + visibleLastCharacter;
                }
                else
                {
                    return fullMedicareId; // If the ID is less than 7 characters, don't hide anything.
                }
            }
        }

        public string Ssn { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Suffix { get; set; }

        public DateTime DateofBirth { get; set; }

        public string Gender { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string State { get; set; }

        public string AgentName { get; set; }
        public string AgentBranch { get; set; }
        public int AgentId { get; set; }

        public int LeadId { get; set; }

        //For Showing Status
        public string Status { get; set; }
        public string EvStatus { get; set; }

        public DateTime CreatedAt { get; set; }
        public string LeadReferenceId { get => "REF" + LeadId.ToString().PadLeft(6, '0'); }
        public bool IsDuplicate { get; set; }

        public string ProductName { get; set; }

    }
    public class GetLeadProductQuestionsDto
    {
        public string ProductName { get; set; }
    }
    public class GetLeadQuestionAnsDto
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public string ProductName { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
    public class GetLeadStatusDto
    {
        public string LeadStatus { get; set; }
        public string AgentName { get; set; }
        public DateTime CreatedAt { get; set; }
    }


    public class GetNewLeadDetailsDto
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public string RefrenceCode { get => "SJ" + ID.ToString().PadLeft(6, '0'); }
        public string MedicareID { get; set; }
        public string Ssn { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Suffix { get; set; }

        public DateTime DateofBirth { get; set; }

        public string Gender { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string State { get; set; }
        public int StateId { get; set; }

        public string AgentName { get; set; }
        public string AgentBranch { get; set; }
        public int AgentId { get; set; }

        public int LeadId { get; set; }
        public string LeadReferenceId { get => "REF" + LeadId.ToString().PadLeft(4, '0'); }
        public string Notes { get; set; }
        public string OnBehalf { get; set; }

        public List<GetLeadStatusDto> LeadStatus { get; set; }
        public List<GetLeadProductQuestionsDto> Products { get; set; }
        public List<GetLeadQuestionAnsDto> QuesAns { get; set; }
        public List<GetEligibilityForDetailsDto> Eligibilities { get; set; }
        public List<GetPenaltyForDetailsDto> Penalties { get; set; }
        public List<GetLeadFileForDetails> Files { get; set; }
        public bool IsDuplicate { get; set; }


    }
    public class GetEligibilityForDetailsDto
    {
        public string FK_StatusId { get; set; }
        public string Status { get; set; }

        public string PrimaryInsurance { get; set; }

        public string HCPSCode { get; set; }

        public string ElgibilityRemarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

    }
    public class GetPenaltyForDetailsDto
    {
        public int Amount { get; set; }

        public string Reason { get; set; }

        public string PenaltyTo { get; set; }

        public DateTime CreatedAt { get; set; }

        public string PenaltyFrom { get; set; }
    }

    public class GetLeadFileForDetails
    {
        public string File { get; set; }
        public string FileType { get; set; }
    }

    public class EditEligibilityDto
    {
        [Required, StringLength(100), Display(Name = "Primary Insurance")]
        public string PrimaryInsurance { get; set; }

        [Required, Display(Name = "Eligibility Status")]
        public int? FK_StatusId { get; set; }

        [StringLength(275), Display(Name = "HCPS Code")]
        public string HCPSCode { get; set; }

        [StringLength(575), Display(Name = "EV Remarks")]
        public string ElgibilityRemarks { get; set; }

        [Required]
        public int FK_LeadID { get; set; }
    }


}
