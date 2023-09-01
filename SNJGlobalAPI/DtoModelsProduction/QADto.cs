using Newtonsoft.Json;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DbModelsProduction;
using SNJGlobalAPI.DtoModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace SNJGlobalAPI.DtoModelsProduction
{
    public class GetQAListDto   
    {
        public GetPatientForSnsDto Patient { get; set; }
        public GetLeadForSnsDto Lead { get; set; }
        public string EvStatus { get; set; }
        public string SnsStatus { get; set; }
        public string QaStatus { get; set; }
        public CheckQaAssignedLeads Assigned { get; set; }
        public string ProductName { get; set; }
        public string ChassingStatus { get; set; }
    }

    public class GetQAListForAgentDto
    {
        public GetPatientForSnsDto Patient { get; set; }
        public GetLeadForSnsDto Lead { get; set; }
        public string EvStatus { get; set; }
        public string SnsStatus { get; set; }
        public string QaStatus { get; set; }
        public string ChassingStatus { get; set; }
    }

    public class GetQaDetailDto
    {
        public GetQaDetailDto()
        {
            SubProducts = new();
            QaQuestions = new();
        }
        public GetPatientForSnsDto Patient { get; set; }
        public GetLeadForSnsDto Lead { get; set; }
        public List<GetEligibilityForDetailsDto> Eligibilities { get; set; }
        public GetSnsForQaDto Sns { get; set; }
        public List<GetSubProductsInLeadForQaDto> SubProducts { get; set; }

        public List<GetQAForLeadDto> Qas { get; set; }
        public List<GetProductQuestionForQaDto> QaQuestions { get; set; }
        public List<GetLeadQuestionAnsDto> FirstStageQuesAns { get; set; }
        public List<GetLeadQuestionAnsDto> FourStageQuesAns { get; set; }
        public List<GetProcessedForExcelDto> Chassing { get; set; }
    }
    public class GetSnsForQaDto
    {
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Remarks { get; set; }
    }

    public class GetProductQuestionForQaDto
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public string ProductName { get; set; }
    }

    public class GetQAForLeadDto
    {
        public string Status { get; set; }
      
        public string Remarks { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<GetQaAnswerForLeadDto> Answers { get; set; }

    }
    public class GetQaAnswerForLeadDto
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string ProductName { get; set; }
    }

    public class GetSubProductsInLeadForQaDto
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ProductName { get; set; }
    }

    public class GetSelectedProductDto
    {
        public int ID { get; set; }
    }

    public class GetLeadProductIdDto
    {
        public int ID { get; set; }
    }

    public class AddQaDto
    {
        public int? Fk_StatusId { get; set; }
       
        public int? Fk_LeadID { get; set; }
        public string Remarks { get; set; }
        public string GetQuestionAnswers { get; set; }
        public int? Penalty { get; set; }
        public string? PenaltyReason { get; set; }
        public List<AddQaQuestionAnswer> QuestionAnswers { get => !String.IsNullOrEmpty(GetQuestionAnswers) ? JsonConvert.DeserializeObject<List<AddQaQuestionAnswer>>(GetQuestionAnswers) : null;}
        [NotMapped]
        public List<IFormFile> QaFiles { get; set; }
        public int? AgentId { get; set; }
        public int? CurrentevStatus { get; set; }
    }


    public class UploadFileDto
    {
        public IFormFile File { get; set; }
        public int CurrentChunk { get; set; }
        public int TotalChunks { get; set; }
    }

    public class AddQaQuestionAnswer
    {
        public int? FK_QuestionID { get; set; }

        public string Answer { get; set; }
    }

    public class CheckQaAssignedLeads
    {
        public string Agent { get; set; }
        public string SuperVisor { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
