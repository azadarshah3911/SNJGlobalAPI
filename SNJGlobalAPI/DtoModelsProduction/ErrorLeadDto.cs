namespace SNJGlobalAPI.DtoModelsProduction
{
    public class GetErrorLeadListDto 
    {
        public GetPatientForSnsDto Patient { get; set; }
        public GetLeadForSnsDto Lead { get; set; }
        public string EvStatus { get; set; }
        public string SnsStatus { get; set; }
        public string QaStatus { get; set; }
        public int StageId { get; set; }
        public string Stage { get; set; }
        public bool IsDuplicate { get; set; }
    }
}
