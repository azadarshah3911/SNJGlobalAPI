using System.Diagnostics.Contracts;

namespace SNJGlobalAPI.DtoModelsProduction
{
    public class AddChassingDto
    {
        public int? Fk_LeadId { get; set; }
        public int? Fk_StatusId { get; set; }
        public string Notes { get; set; }

        public List<IFormFile> ChassingFiles { get; set; }
    }

    public class GetProcessedForExcelDto
    {
        public string Status { get; set; }
        public int? Fk_StatusId { get; set; }
        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }

    public class GetProcessedDetailsDto : GetQaDetailDto
    {
        public List<GetProcessedForExcelDto> Chassing { get; set; }
        public List<string> ChassingFiles { get; set; }
    }

}
