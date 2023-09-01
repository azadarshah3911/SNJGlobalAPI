using AutoMapper;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.DtoModelsProduction;

namespace SNJGlobalAPI.Mappers
{
    public class CommonMapper : Profile
    {
        public CommonMapper()
        {
            CreateMap<SignupDto, User>();
        }

       /* public static MapperConfiguration GetBonus()
       
            =>new(
            c =>
            {
                c.CreateProjection<UserBonus, GetBonusDto>();
            });*/

    }
}