using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNJGlobalAPI.DtoModels
{
    public class UserAuthDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Branch { get; set; }
        public string Image { get; set; }

        public List<string> Roles { get; set; }
    }

    public class SigninDto
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class SignupDto
    {
        [Required, StringLength(275), Display(Name = "First Name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string FirstName { get; set; }

        [StringLength(275), Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string LastName { get; set; }

        [Required, EmailAddress(ErrorMessage = "Please provide valid email"), StringLength(255)]
        public string Email { get; set; }

        [Required, StringLength(575), Display(Name = "Password")]
        public string Password { get; set; }

        [Required, Display(Name = "Branch")] 
        public int Fk_BranchId { get; set; }

        public ICollection<int> Roles { get; set; }

        public IFormFile ProfileImage { get; set; }
        public string NicNo { get; set; }
        [DataType(DataType.Date)]
        public DateTime? JoiningDate { get; set; }
        public string Gender { get; set; }
        public string EmployeeId { get; set; }
        public string EmergencyContact { get; set; }
        public string Address { get; set; }
    }

    public class UserUpdateForUserDto
    {
        [Required, Range(1, int.MaxValue)]
        public int Id { get; set; }

        [StringLength(275), Display(Name = "First Name")]
        //[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string FirstName { get; set; }

        [StringLength(275), Display(Name = "Last Name")]
        //[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string LastName { get; set; }

        [Required, EmailAddress(ErrorMessage = "Please provide valid email"), StringLength(255)]
        public string Email { get; set; }

        [Required, Display(Name = "Branch")]
        public int Fk_BranchId { get; set; }

        public IFormFile Image { get; set; }

        [StringLength(255)]
        public string PhoneNumber { get; set; }
        public string NicNo { get; set; }
        [DataType(DataType.Date)]
        public DateTime? JoiningDate { get; set; }
        public string Gender { get; set; }
        public string EmployeeId { get; set; }
        public string EmergencyContact { get; set; }
        public string Address { get; set; }
        [StringLength(255)]
        public string Password { get; set; }
    }

    public class UserUpdateDto
    {
        [Required, Range(1, int.MaxValue)]
        public int Id { get; set; }

        [StringLength(275), Display(Name = "First Name")]
        //[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string FirstName { get; set; }

        [StringLength(275), Display(Name = "Last Name")]
        //[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Please provide valid email"), StringLength(255)]
        public string Email { get; set; }

        [Display(Name = "Branch")]
        public int Fk_BranchId { get; set; }

        public IFormFile Image { get; set; }

        [StringLength(255)]
        public string PhoneNumber { get; set; }
        public string NicNo { get; set; }
        [DataType(DataType.Date)]
        public DateTime? JoiningDate { get; set; }
        public string Gender { get; set; }
        public string EmployeeId { get; set; }
        public string EmergencyContact { get; set; }
        public string Address { get; set; }
        [StringLength(255)]
        public string Password { get; set; }
        public ICollection<int> Roles { get; set; }

    }

    public class UserResetDto
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsActivated { get; set; }
    }

    public class UserLockDto
    {
        public DateTime? LockedOn { get; set; }
    }

    public class UserGetDtoForAdmin
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Branch { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public byte LoginFailedCount { get; set; }
        public DateTime? LockedOn { get; set; }
        public DateTime? LoginFailedOn { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public bool IsActivated { get; set; }
        public DateTime? ActDctAt { get; set; }
        public string ActDctBy { get; set; }

        public string ActDctReason { get; set; }

        public ICollection<RolesGetDto> Roles { get; set; }

        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public string NicNo { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string Gender { get; set; }
        public string EmployeeId { get; set; }
        public string EmergencyContact { get; set; }
        public string Address { get; set; }
        public int Fk_BranchId { get; set; }
    }

    public class RolesGetDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class RolesGet1Dto
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class UserGetDtoForUser
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Fk_BranchId { get; set; }
        public string Branch { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public string NicNo { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string Gender { get; set; }
        public string EmployeeId { get; set; }
        public string EmergencyContact { get; set; }
        public string Address { get; set; }
    }

    //For checking user activation/deactivation from db
    public class UserActDctOutputDto
    {
        public bool IsActivated { get; set; }
    }

    //for getting input to activate/deactivate user
    public class UserActDctInputDto
    {
        public int UserId { get; set; }
        public string Reason { get; set; }
    }

    //For confirming account
    public class EmailDto
    {
        [Required(ErrorMessage = "Please enter email"), StringLength(255)]
        public string Email { get; set; }

    }
    //for resetting password
    public class NewPswdDto
    {
        [Required(ErrorMessage = "Please attach record id")]
        public Guid ResetId { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("Password", ErrorMessage = "Password doesnot match")]
        public string ConfirmPassword { get; set; }
    }

    public class GetUserBranchDto 
    {
        public string Name { get; set; }
    }


    public class AuthorizationDto
    {
        public bool ShowDashboard { get; set; }
        public bool ShowLeads { get; set; }
        public bool ShowAllLeads { get; set; }
        public bool ShowUploadLead { get; set; }
        public bool ShowEligibility { get; set; }
        public bool ShowSns { get; set; }
        public bool ShowQa { get; set; }
        public bool ShowChassing { get; set; }
        public bool ShowVerification { get; set; }
        public bool ShowCgmAndBraces { get; set; }
        public bool ShowConfirmation { get; set; }
    }

    public class appRolesNameDto
    {
        public const string SuperAdmin = "SuperAdmin"; 
        public const string Agent = "Agent";
        public const string ProductionManager = "Production Manager";
        public const string QaManager = "Qa Manager";
        public const string QaAgent = "Qa Agent";
        public const string ChassingManager = "Chassing Manager";
        public const string ChassingAgent = "Chassing Agent";
        public const string ChassingVerificationAgent = "Chassing Verification Agent";
        public const string TeamLead = "Team Lead";
        public const string LeadFixer = "Lead Fixer";
    }

    public class SignupInListDto
    {
        [Required, StringLength(275), Display(Name = "First Name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string FirstName { get; set; }

        [StringLength(275), Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string LastName { get; set; }

        [Required, EmailAddress(ErrorMessage = "Please provide valid email"), StringLength(255)]
        public string Email { get; set; }

        [Required, StringLength(575), Display(Name = "Password")]
        public string Password { get; set; }

        [Required, Display(Name = "Branch")]
        public int Fk_BranchId { get; set; }

        public ICollection<int> Roles { get; set; }
    }
}