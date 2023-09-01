using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DtoModelsProduction
{
    public class AddLeadCommentDto
    {
        [Required]
        public string Comment { get; set; }
        [Required]
        public int Fk_StageId { get; set; }
        [Required]
        public int FK_LeadID { get; set; }
     }

    public class GetLeadCommentDto
    {
        public string Comment { get; set; }
        public string CreatedBy { get; set; }
        public string Stage { get; set; }
    }
}
