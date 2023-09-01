using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SNJGlobalAPI.DbModelsProduction;

namespace SNJGlobalAPI.DbModels
{
    public class User
    {
        #region User Model
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required, StringLength(275), Display(Name = "First Name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string FirstName { get; set; }

        [StringLength(275), Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string LastName { get; set; }
        [Required, EmailAddress(ErrorMessage = "Please provide valid email"), StringLength(255)]
        public string Email { get; set; }

        //[Required,DataType(DataType.Password), StringLength(575), Display(Name = "Password")]
        //public string Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

        
        public int? Fk_BranchId { get; set; }
        [ForeignKey("Fk_BranchId")]
        public Branch branch { get; set; }
         
        public int? Fk_CreatedBy { get; set; }
        [ForeignKey("Fk_CreatedBy")]
        public User creator { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public byte LoginFailedCount { get; set; } = 0;
        public DateTime? LockedOn { get; set; }
        public DateTime? LoginFailedOn { get; set; }

        public int? Fk_UpdatedBy { get; set; }
        [ForeignKey("Fk_UpdatedBy")]
        public User updator { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public bool IsActivated { get; set; } = true;
        public DateTime? ActDctAt { get; set; }
        public int? Fk_ActDctBy { get; set; }
        [ForeignKey("Fk_ActDctBy")]
        public User actDctBy { get; set; }
        public string Reason { get; set; }


        public int? Fk_DeletedBy { get; set; }
        [ForeignKey("Fk_DeletedBy")]
        public User deletor { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public string NicNo { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string Gender { get; set; }
        public string EmployeeId { get; set; }
        public string EmergencyContact { get; set; }
        public string Address { get; set; }

        #endregion

        #region User List
        //relation itself
        public ICollection<User> userCreators { get; set; }
        public ICollection<User> userUpdators { get; set; }
        public ICollection<User> userDeletors { get; set; }
        public ICollection<User> userActDcts { get; set; }

        //userRoles
        public ICollection<UserRole> usersRoles { get; set; }
        public ICollection<UserRole> userRolesCreators { get; set; }

        //user login
        public ICollection<UserLogin> usersLogins { get; set; }
        //user forget password
        public ICollection<ForgetPassword> forgetPasswords { get; set; }
#endregion

        #region Lead List
        public ICollection<Lead> Lead { get; set; }
        public ICollection<Lead> DeletedByLead { get; set; }
        public ICollection<LeadProduct> LeadProducts { get; set; }
        public ICollection<LeadSubProduct> LeadSubProducts { get; set; }
        public ICollection<LeadFile> LeadFiles { get; set; }
        public ICollection<LeadComments> LeadComments { get; set; }
        public ICollection<LeadStatus> LeadStatuses { get; set; }
        #endregion

        #region Product List
        public ICollection<SubProduct> SubProducts { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Product> CreatedProducts { get; set; }
        public ICollection<Product> UpdatedProducts { get; set; }
        public ICollection<Product> DeletedProducts { get; set; }
        public ICollection<ProductQuestionAnswer> ProductQuestionAnswer { get; set; }
        #endregion

        #region Eligibility List
        public ICollection<Eligibility> Eligibilities { get; set; }
        #endregion

        #region Patient List
        public ICollection<Patient> CreatedPatients { get; set; }
        public ICollection<Patient> UpdatedPatients { get; set; }
        public ICollection<Patient> DeletedPatients { get; set; }
        public ICollection<PatientLogs> PatientLogs { get; set; }
        #endregion
        
        #region Penalty List
        public ICollection<AgentPenalty> PenaltyTo { get; set; }
        public ICollection<AgentPenalty> PenaltyFrom { get; set; }
        #endregion

        #region SNS List
        public ICollection<SNS> CreatedSNS { get; set; }
        public ICollection<SNS> UpdatedSNS { get; set; }
        #endregion

        #region QA List
        public ICollection<QA> CreatedByQA { get; set; }
        public ICollection<QaQuestionAnswer> CreatedQaAnswers { get; set; }
        public ICollection<QAFiles> QAFiles { get; set; }
        #endregion

        #region Lead Assigned
        public ICollection<LeadAssigned> LeadAssignedsTo { get; set; }
        public ICollection<LeadAssigned> LeadAssignedsFrom { get; set; }

        #endregion

        #region Bonus
        public ICollection<UserBonus> BonusesTo { get; set; }
        public ICollection<UserBonus> BonusesFrom { get; set; }
        #endregion

        #region Chassing
        public ICollection<Chassing> Chassings { get; set; }
        public ICollection<ChassingFile> ChassingFiles { get; set; }
        public ICollection<ChassingVerification> ChassingVerifications { get; set; }
        #endregion


        #region Chassing
        public ICollection<Confirmation> Confirmations { get; set; }

        #endregion

    }
}
