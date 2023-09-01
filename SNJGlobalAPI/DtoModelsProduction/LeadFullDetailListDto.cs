namespace SNJGlobalAPI.DtoModelsProduction
{
    public class LeadFullDetailListDto
    {
        public LeadFullDetailListDto()
        {
            SubProducts = new();
            Question = new();
        }
        public GetPatientForSnsDto Patient { get; set; }
        public GetLeadForSnsDto Lead { get; set; }
        public GetEligibilityForDetailsDto Eligibilities { get; set; }
        public GetSnsForQaDto Sns { get; set; }
        public List<GetSubProductsInLeadForQaDto> SubProducts { get; set; }

        public GetQAForLeadDto Qas { get; set; }
        public List<GetProductQuestionForQaDto> Question { get; set; }

        public List<GetLeadQuestionAnsDto> FirstStageQuesAns { get; set; }

        public List<string> QaFiles { get; set; }
        public List<string> LeadFiles { get; set; }
        public List<GetProcessedForExcelDto> Chassing { get; set; }
        public List<string> ChassingFiles { get; set; }
    }




}
