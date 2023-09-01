using AutoMapper;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Mappers
{
    public class AgentHistoryMapper
    {
        public static MapperConfiguration GetPenalty = new
            (proj =>
            {
                proj.CreateProjection<AgentPenalty, GetAgentPenaltyDto>()
                .ForMember(member => member.LeadId, source => source.MapFrom(map => map.Fk_LeadID))
                .ForMember(member => member.PatientId, source => source.MapFrom(map => map.Lead.Patient.ID))
                .ForMember(member => member.PenaltyFrom, source => source.MapFrom(map => map.PenaltyFrom.FirstName + " " + map.PenaltyFrom.LastName))
                .ForMember(member => member.AgentName, source => source.MapFrom(map => map.PenaltyTo.FirstName + " " + map.PenaltyFrom.LastName))
                .ForMember(member => member.Stage, source => source.MapFrom(map => map.Stage.Name))
               .ForMember(member => member.AgentBranch, source => source.MapFrom(map => map.PenaltyTo.branch.Name))
                .ForMember(member => member.AgentId, source => source.MapFrom(map => map.PenaltyTo.ID));
            });

        public static MapperConfiguration GetBonus = new
           (proj =>
           {
               proj.CreateProjection<UserBonus, GetAgentBonusDto>()
               .ForMember(member => member.BonusFrom, source => source.MapFrom(map => map.BonusFrom.FirstName + " " + map.BonusFrom.LastName))
               .ForMember(member => member.AgentName, source => source.MapFrom(map => map.BonusTo.FirstName + " " + map.BonusTo.LastName))
               .ForMember(member => member.AgentBranch, source => source.MapFrom(map => map.BonusTo.branch.Name))
               .ForMember(member => member.AgentId, source => source.MapFrom(map => map.BonusTo.ID));
           });
    }
}
