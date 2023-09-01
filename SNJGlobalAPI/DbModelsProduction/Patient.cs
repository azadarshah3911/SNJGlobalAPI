using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using SNJGlobalAPI.DbModelsProduction;

namespace SNJGlobalAPI.DbModels
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(11), MinLength(11), Display(Name = "Medicare Id")]
        //[RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "Please enter a valid Medicare ID")]
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

        public int? FK_StateId { get; set; }
        [ForeignKey("FK_StateId")]
        public State State { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? FK_CreatedBy { get; set; }
        [ForeignKey("FK_CreatedBy")]
        public User CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public int? FK_UpdatedBy { get; set; }
        [ForeignKey("FK_UpdatedBy")]
        public User UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }
        public int? FK_DeletedBy { get; set; }
        [ForeignKey("FK_DeletedBy")]
        public User DeletedBy { get; set; }

        public ICollection<Lead> Leads { get; set; }
        public ICollection<PatientLogs> PatientLogs { get; set; }
    }
}
