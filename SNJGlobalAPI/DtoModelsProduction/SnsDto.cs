using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Security.Principal;
using SNJGlobalAPI.DtoModels;

namespace SNJGlobalAPI.DtoModelsProduction
{
    public class AddSnsDto
    {
        public int? Id { get; set; }

        [Required,Display(Name = "SNS Check")]
        public int? Fk_StatusId { get; set; }
        public string Remarks { get; set; }
        [Required]
        public int Fk_LeadId { get; set; }
        public List<AddSubProductSnsDto> SubProducts { get; set; }
    }


    public class UpdateSnsDto
    {
        [Required]
        public int? Id { get; set; }

        [Required, Display(Name = "SNS Check")]
        public int? Fk_StatusId { get; set; }
        public string Remarks { get; set; }
        [Required]
        public int Fk_LeadId { get; set; }
        public List<AddSubProductSnsDto> SubProducts { get; set; }
    }

    public class AddSubProductSnsDto
    {   
        public int SubProductId { get; set; }
        public bool Selected { get; set; }
    }

    //SNS Request Fo List
    public class GetSnsPendingAndByPassListDto
    {
        public GetPatientForSnsDto Patient { get; set; }
        public GetLeadForSnsDto Lead { get; set; }
        public string EvStatus { get; set; }
        public string SnsStatus { get; set; }
    }

    //Lead
    public class GetLeadForSnsDto
    {
        public string AgentName { get; set; }
        public string AgentBranch { get; set; }
        public int AgentId { get; set; }

        public int LeadId { get; set; }
        public string LeadReferenceId { get => "REF" + LeadId.ToString().PadLeft(6, '0'); }
        public string LeadStatus { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDuplicate { get; set; }
        public string ProductName { get; set; }
        public string OnBehalf { get; set; }
    }

    //Patient
    public class GetPatientForSnsDto 
    {
        public int ID { get; set; }
        public string RefrenceCode { get => "SJ" + ID.ToString().PadLeft(4, '0'); }
        public string MedicareID { get; set; }
        public string _MedicareId { get
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
        public string StateCode { get; set; }
        public string Fk_StateId { get; set; }
        public string Ssn { get; set; }
    }


    public class GetSnsDetailDto
    {
        public GetSnsDetailDto()
        {
            SubProductsOfProduct = new();
        }
        public GetPatientForSnsDto Patient { get; set; }
        public GetLeadForSnsDto Lead { get; set; }
        public List<GetEligibilityForDetailsDto> Eligibilities { get; set; }
        public List<GetProductIdDto> Products { get; set; }
        public string SnsStatus { get; set; }
       // public List<GetSubProductsInLeadDto> SubProducts { get; set; }

        public GetSnsForSnsDto Sns { get; set; }

        public List<GetSubProductOfProductsDto> SubProductsOfProduct { get; set; }

        public List<GetLeadQuestionAnsDto> QuesAns { get; set; }
    }
    public class GetProductIdDto
    {
        public int Id { get; set; }
    }

    public class GetSnsForSnsDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Remarks { get; set; }
    }

}
