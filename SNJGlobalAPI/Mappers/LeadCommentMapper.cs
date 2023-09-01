using AutoMapper;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Mappers
{
    public class LeadCommentMapper
    {
        public static MapperConfiguration GetLeadComments = new(
            c =>
            {
                c.CreateProjection<LeadComments, GetLeadCommentDto>()
                .ForMember(s => s.CreatedBy, o => o.MapFrom(m => m.User.FirstName+ " "+m.User.LastName))
                .ForMember(s => s.Stage, o => o.MapFrom(m => m.Stage.Name));
            }
            );
    }
}
