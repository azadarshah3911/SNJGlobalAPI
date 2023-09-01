using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SNJGlobalAPI.DtoModelsProduction
{
   public class AddLeadDto
    {
        public int? PatientID { get; set; }
        
        [Display(Name = "Medicare Id")]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "Please enter a valid Medicare ID")]
        public string MedicareID { get; set; }
        public string Ssn { get; set; }

        [StringLength(275), Display(Name = "First Name")]
        //[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string FirstName { get; set; }

        [StringLength(275), Display(Name = "Middle Name")]
        //[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Pleas,e enter a valid name")]
        public string MiddleName { get; set; }

        [StringLength(275), Display(Name = "Last Name")]
        //[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string LastName { get; set; }

        [StringLength(10), MinLength(10), Display(Name = "Phone Number"), Required(ErrorMessage = "Phone Number Required")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid US phone number.")]
        public string PhoneNumber { get; set; }

        [StringLength(50), Display(Name = "Suffix")]
        public string Suffix { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime DateofBirth { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [StringLength(5), Display(Name = "ZIP Code")]
        public string ZipCode { get; set; }

        [StringLength(575), Display(Name = "City")]
        public string City { get; set; }

        [StringLength(575), Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(575), Display(Name = "Address")]
        public string Address2 { get; set; }
        public int FK_StateId { get; set; }
        public List<int> SubProducts { get; set; }
        public List<AddLeadQuestionDto> Questions { get; set; }
        public string Notes { get; set; }
        public string OnBehalf { get; set; }
    }

    public class AddLeadQuestionDto
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }

    public class GetLeadDetailsDto : GetQaDetailDto
    {
        public List<GetLeadStatusesDto> LeadStatuses { get; set; }
        public List<GetAgentPenaltyDto> Penalties { get; set; }
        public List<string> QaFiles { get; set; }
        public List<string> LeadFiles { get; set; }
        public List<GetPateintLogsDto> PatientLogs { get; set; }

        public List<GetProcessedForExcelDto> Chassing { get; set; }
        public List<string> ChassingFiles { get; set; }
        /* public List<GetQuestionAnswerForLeadDto> LeadQuestionAnswer { get; set; }*/
    }

    public class GetLeadStatusesDto
    {
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }


    public class GetQuestionAnswerForLeadDto
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string ProductName { get; set; }

        public GetAnswerForLeadDto Answer { get; set; }
    }

    public class GetAnswerForLeadDto
    {
        public int AnswerId { get; set; }
        public string Answer { get; set; }
    }

    public class GetPateintLogsDto : GetPatientForSnsDto
    {
        public string LogCreatedBy { get; set; }
        public DateTime LogCreatedAt { get; set; }
        public string LogStage { get; set; }
        public int LeadId { get; set; }
        public string LeadReferenceId { get => "REF" + LeadId.ToString().PadLeft(4, '0'); }
    }
    public class leadListDto : AllStatusDto
    {
        public int Id { get; set; }
        public string LeadReferenceId { get => "REF" + Id.ToString().PadLeft(4, '0'); }
        public string AgentName { get; set; }
        public string AgentBranch { get; set; }
        public DateTime CreatedAt { get; set; }
        public GetPatientForSnsDto Patient { get; set; }
        public bool IsDuplicate { get; set; }
        public string ProductName { get; set; }

    }

    public class AllStatusDto
    {
        public string LeadStatus { get; set; }
        public string EvStatus { get; set; }
        public string SnsStatus { get; set; }
        public string QaStatus { get; set; }
        public string ChassingStatus { get; set; }
        public string ConfiramtionStatus { get; set; }
        public string ChassingVerificationStatus { get; set; }
        public string Confirmation { get; set; }
    }

}
