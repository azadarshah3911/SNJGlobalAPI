using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SNJGlobalAPI.DtoModelsProduction
{
    public class GetStateDdDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class GetStatusDdDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int StageId { get; set; }
    }
    public class GetAgentDdDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<GetDdRolesForUserDto> Roles { get; set; }
    }

    public class GetDdRolesForUserDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
