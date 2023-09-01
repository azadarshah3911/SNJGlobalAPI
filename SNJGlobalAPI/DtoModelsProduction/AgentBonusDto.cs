namespace SNJGlobalAPI.DtoModelsProduction
{
    public class AddAgentBonusDto
    {
        public int Amount { get; set; }
        public int Fk_BonusTo { get; set; }
    }

    public class GetAgentBonusDto
    {
        public int Amount { get; set; }
        public string AgentName { get; set; }
        public string AgentBranch { get; set; }
        public string BonusFrom { get; set; }
        public int AgentId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
