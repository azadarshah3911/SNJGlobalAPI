namespace SNJGlobalAPI.DtoModelsProduction
{
    public class GetAgentPenaltyDto
    {
        public int PatientId { get; set; }
        public string RefrenceCode { get => "SJ" + PatientId.ToString().PadLeft(4, '0'); }
        public int Amount { get; set; }
        public string Reason { get; set; }
        public string AgentName { get; set; }
        public string PenaltyFrom { get; set; }
        public int LeadId { get; set; }
        public string LeadReferenceId { get => "SJ" + LeadId.ToString().PadLeft(4, '0'); }
        public int AgentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Stage { get; set; }
        public string AgentBranch { get; set; }
    }

    
}
