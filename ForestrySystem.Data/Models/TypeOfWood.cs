using ForestrySystem.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ForestrySystem.Models
{
    public class TypeOfWood
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Name of species")]
        public string SpeciesName { get; set; }
        public OriginEnum Origin { get; set; }
        [DisplayName("Amount for logging")]
        public float AmountForLogging { get; set; }
        [DisplayName("Year of logging")]
        public DateTime YearOfLogging { get; set; }

    }
}
