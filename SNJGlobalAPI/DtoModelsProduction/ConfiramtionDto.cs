using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DtoModelsProduction
{
    public class AddConfiramtionDto
    {
        [Required]
        public string Notes { get; set; }
        [Required]
        public int? Fk_LeadId { get; set; }
        [Required]
        public int? Fk_StatusId { get; set; }
    }

}
