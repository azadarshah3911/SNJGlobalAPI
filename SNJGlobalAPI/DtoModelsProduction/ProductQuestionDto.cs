using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNJGlobalAPI.DtoModelsProduction
{
    public class GetProductQuestionForAgentDto
    {
        public int ID { get; set; }
        public string Question { get; set; }
    }

    public class GetProductsAndSendQuestionsDto
    {
        public int[] ProductsId { get; set; }
    }

    public class EditProductQuestionAnswerDto
    {
        public int Id { get; set; }
        public string Answer { get; set; }
    }
}
