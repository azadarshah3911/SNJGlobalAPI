using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNJGlobalAPI.DbModelsProduction
{
    public class ProductQuestion
    {
        [Key]
        public int ID { get; set; }
        public int? FK_ProductId { get; set; }
        [ForeignKey("FK_ProductId")]
        public Product Product { get; set; }
        
        public int? FK_StageId { get; set; }
        [ForeignKey("FK_StageId")]
        public Stage Stage { get; set; }

        public string Question { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }
        public ICollection<ProductQuestionAnswer> ProductQuestionAnswer { get; set; }
        public ICollection<QaQuestionAnswer> QaQuestionAnswers { get; set; }


    }

   
}
