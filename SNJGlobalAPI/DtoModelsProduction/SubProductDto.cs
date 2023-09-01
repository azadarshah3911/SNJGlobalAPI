using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DtoModelsProduction
{
    public class GetSubProductForAgentDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? FK_ProductId { get; set; }
    }
}
