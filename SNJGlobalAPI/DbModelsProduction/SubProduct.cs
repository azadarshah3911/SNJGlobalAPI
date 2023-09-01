using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNJGlobalAPI.DbModels
{
    public class SubProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
     
        [StringLength(500)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        
        public int? FK_ProductId { get; set; }
        [ForeignKey("FK_ProductId")]
        public Product Product { get; set; }
        
        public DateTime? CreatedAt { get; set; }
        public int? FK_CreatedBy { get; set; }
        [ForeignKey("FK_CreatedBy")]
        public User CreatedBy { get; set; }

        //List
        public ICollection<LeadProduct> LeadProducts { get; set; }

        //By Azadar
        public bool IsParent { get; set; }

    }
}
