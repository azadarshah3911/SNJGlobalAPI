namespace SNJGlobalAPI.DtoModelsProduction
{
    public class PdfDto
    {
        public GetPatientForSnsDto Patient { get; set; }
        public GetEligibilityForDetailsDto Eligibilities { get; set; }
        public GetQAForLeadDto Qas { get; set; }
        public List<GetLeadQuestionAnsDto> FirstStageQuesAns { get; set; }
        public List<GetSubProductsInLeadForQaDto> SubProducts { get; set; }

    }
}
