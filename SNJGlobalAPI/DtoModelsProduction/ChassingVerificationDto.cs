using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNJGlobalAPI.DtoModelsProduction
{
    public class AddChassingVerificationDto
    {
        [Required]
        public string Notes { get; set; }
        [Required]
        public int? Fk_LeadId { get; set; }
        [Required]
        public int? Fk_StatusId { get; set; }
    }
}
