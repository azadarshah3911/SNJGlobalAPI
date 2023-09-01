using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DtoModels
{
    public class AddProductDto
    {
        [StringLength(500),Required]
        public string Name { get; set; }
    }
    public class GetProductDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
    public class ProductionDdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ProductUpdateDto
    {
        [Required(ErrorMessage = "Please attach record id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter speciality name"), StringLength(50)]
        public string Name { get; set; }

    }//insAddDto

    public class GetSubProductsInLeadDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ProductName { get; set; }
    }


    public class GetSubProductOfProductsDto
    {
        public string ProductName { get; set; }
        public List<GetSubProductsDto> SubProducts { get; set; }
    }

    public class GetSubProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsParent { get; set; }
        public string Code { get; set; }
    }
}
