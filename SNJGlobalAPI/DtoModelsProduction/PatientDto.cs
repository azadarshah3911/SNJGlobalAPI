using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using SNJGlobalAPI.DbModelsProduction;

namespace SNJGlobalAPI.DtoModelsProduction
{
    public class AddPatientDto
    {
        [StringLength(11), MinLength(11), Display(Name = "Medicare Id")]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "Please enter a valid Medicare ID")]
        public string MedicareID { get; set; }
        public string Ssn { get; set; }

        [StringLength(275), Display(Name = "First Name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string FirstName { get; set; }

        [StringLength(275), Display(Name = "Middle Name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Pleas,e enter a valid name")]
        public string MiddleName { get; set; }

        [StringLength(275), Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string LastName { get; set; }

        [StringLength(10), MinLength(10), Display(Name = "Phone Number"), Required(ErrorMessage = "Phone Number Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid US phone number.")]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(50), Display(Name = "Suffix")]
        public string Suffix { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime DateofBirth { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [StringLength(5),MinLength(5), Display(Name = "ZIP Code")]
        public string ZipCode { get; set; }

        [StringLength(575), Display(Name = "City")]
        public string City { get; set; }

        [StringLength(575), Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(575), Display(Name = "Address")]
        public string Address2 { get; set; }
        public int FK_StateId { get; set; }
    }

    public class GetPatientDto
    {
        public int ID { get; set; }
        public string MedicareID { get; set; }
        public string Ssn { get; set; }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string Suffix { get; set; }

        public DateTime DateofBirth { get; set; }

        public string Gender { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }
        public int FK_StateId { get; set; }
        public string State { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FK_CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }    
        public string FK_UpdatedBy { get; set; }
    }

    public class UpdatePatientDto  
    {
        [Required(ErrorMessage = "Id Required")]
        public int ID { get; set; }

        [StringLength(11), MinLength(11), Display(Name = "Medicare Id")]
       /* [RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "Please enter a valid Medicare ID")]*/
        public string MedicareID { get; set; }
        public string Ssn { get; set; }

        [StringLength(275), Display(Name = "First Name")]
       /* [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]*/
        public string FirstName { get; set; }

        [StringLength(275), Display(Name = "Middle Name")]
      /*  [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Pleas,e enter a valid name")]*/
        public string MiddleName { get; set; }

        [StringLength(275), Display(Name = "Last Name")]
      /*  [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]*/
        public string LastName { get; set; }

        [StringLength(10), MinLength(10), Display(Name = "Phone Number")]
      /*  [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid US phone number.")]*/
        public string PhoneNumber { get; set; } = null!;

        [StringLength(50), Display(Name = "Suffix")]
        public string Suffix { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime DateofBirth { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [StringLength(5), MinLength(5), Display(Name = "ZIP Code")]
        public string ZipCode { get; set; }

        [StringLength(575), Display(Name = "City")]
        public string City { get; set; }

        [StringLength(575), Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(575), Display(Name = "Address")]
        public string Address2 { get; set; }
        public int FK_StateId { get; set; }
        public List<EditProductQuestionAnswerDto> QuestionAnswer { get; set; }
    }

    public class GetPatientFoAgentDto
    {
        public int ID { get; set; }
        public string MedicareID { get; set; }
      /*  public string Ssn { get; set; }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string Suffix { get; set; }

        public DateTime DateofBirth { get; set; }

        public string Gender { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }
        public int FK_StateId { get; set; }*/
    }

    public class GetPatientForAgenBySsntDto
    {
        public int ID { get; set; }
        public string Ssn { get; set; }
    }

    public class PatientLeadListDto
    {
        public int ID { get; set; }
        public string MedicareID { get; set; }
        public string RefrenceCode { get => "SJ" + ID.ToString().PadLeft(4, '0'); }
        public string Ssn { get; set; }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string Suffix { get; set; }

        public DateTime DateofBirth { get; set; }

        public string Gender { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string State { get; set; }   

        public int LeadCount { get; set; }
    }

    public class CheckPatientSameProductDto
    {
        public int ID { get; set; }
        public List<int> ProductId   { get; set; }
    }

    public class CheckPatientSameSubProductDto
    {
        public int ID { get; set; }
        public int SubProductId { get; set; }
    }

    public class IsDuplicateDto
    {
        public bool Yes { get; set; }
    }


    public class GetEditPatientForAgentDto
    {
        public GetPatientForSnsDto Patient { get; set; }
        public GetLeadForSnsDto Lead { get; set; }
        public List<GetLeadQuestionAnsDto> FirstStageQuesAns { get; set; }
    }
}
