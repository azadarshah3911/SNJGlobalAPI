using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace SNJGlobalAPI.DbModels
{
    public class Eligibility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? FK_LeadID { get; set; }
        [ForeignKey("FK_LeadID")]
        public Lead Lead { get; set; }


        [Required, Display(Name = "Eligibility Status")]
        public int? FK_StatusId { get; set; }
        [ForeignKey("FK_StatusId")]
        public Status Status { get; set; }

        [StringLength(100),Display(Name = "Primary Insurance")]
        public string PrimaryInsurance { get; set; }

       
        [StringLength(275),Display(Name = "HCPS Code")]
        public string HCPSCode { get; set; }

        [StringLength(575),Display(Name = "EV Remarks")]
        public string ElgibilityRemarks { get; set; }
        public int? CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public User User { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
