using ForestrySystem.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ForestrySystem.Models
{
    public class CategoryOfTimber
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Category Name")]
        public TypeOfTimberEnum CategoryName { get; set; }
        [DisplayName("Amount for Logging")]
        public float AmountForLogging { get; set; }
        [DisplayName("Year of Logging")]
        public DateTime YearOfLogging { get; set; }
    }
}
