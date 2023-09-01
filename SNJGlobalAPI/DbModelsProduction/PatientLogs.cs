using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DbModelsProduction
{
    public class PatientLogs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? FK_StateId { get; set; }
        [ForeignKey("FK_StateId")]
        public State State { get; set; }

        public int? FK_PatientID { get; set; }
        [ForeignKey("FK_PatientID")]
        public Patient Patient { get; set; }

        public int? Fk_StageId { get; set; }
        [ForeignKey("Fk_StageId")]
        public Stage Stage { get; set; }

        public int? Fk_LeadId { get; set; }
        [ForeignKey("Fk_LeadId")]
        public Lead Lead { get; set; }


        [StringLength(11), MinLength(11), Display(Name = "Medicare Id"), Required(ErrorMessage = "Medicare Id Required")]
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

        [StringLength(5), Display(Name = "ZIP Code")]
        public string ZipCode { get; set; }

        [StringLength(575), Display(Name = "City")]
        public string City { get; set; }

        [StringLength(575), Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(575), Display(Name = "Address")]
        public string Address2 { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? FK_CreatedBy { get; set; }
        [ForeignKey("FK_CreatedBy")]
        public User CreatedBy { get; set; }
    }
}
