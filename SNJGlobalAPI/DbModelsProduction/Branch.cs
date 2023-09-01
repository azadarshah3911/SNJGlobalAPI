using SNJGlobalAPI.DbModels;
using System.ComponentModel.DataAnnotations;

namespace SNJGlobalAPI.DbModelsProduction
{
    public class Branch
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
         
        public ICollection<User> Agents { get; set; }
    }
}
