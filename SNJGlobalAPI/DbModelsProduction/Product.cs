using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SNJGlobalAPI.DbModelsProduction;

namespace SNJGlobalAPI.DbModels
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public int? FK_CreatedBy { get; set; }
        [ForeignKey("FK_CreatedBy")]
        public User CreatedBy { get; set; }

        [Range(1, 10)]
        public int AllowedStages { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public int? Fk_UpdatedBy { get; set; }
        [ForeignKey("Fk_UpdatedBy")]
        public User UpdatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }
        public int? Fk_DeletedBy { get; set; }
        [ForeignKey("Fk_DeletedBy")]
        public User DeletedBy { get; set; }

        //List
        public ICollection<SubProduct> SubProducts { get; set; }
        public ICollection<LeadProduct> LeadProducts { get; set; }
        public ICollection<LeadSubProduct> LeadSubProducts { get; set; }
        public ICollection<ProductQuestion> ProductQuestions { get; set; }

    }
}
