using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DbModels
{
    public class LeadSubProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        public int? FK_LeadID { get; set; }
        [ForeignKey("FK_LeadID")]
        public Lead Lead { get; set; }
        
        public int? FK_SubProductId { get; set; }
        [ForeignKey("FK_SubProductId")]
        public SubProduct SubProduct { get; set; }

        public int? FK_StagetId { get; set; }
        [ForeignKey("FK_StagetId")]
        public Stage Stage { get; set; }

        public int StageCount { get; set; } = 0;

        public bool IsApproved { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? FK_CreatedBy { get; set; }
        [ForeignKey("FK_CreatedBy")]
        public User CreatedBy { get; set; }
    }

    public class LeadProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? FK_LeadID { get; set; }
        [ForeignKey("FK_LeadID")]
        public Lead Lead { get; set; }

        public int? FK_ProductId { get; set; }
        [ForeignKey("FK_ProductId")]
        public Product Product { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? FK_CreatedBy { get; set; }
        [ForeignKey("FK_CreatedBy")]
        public User CreatedBy { get; set; }
    }
}
