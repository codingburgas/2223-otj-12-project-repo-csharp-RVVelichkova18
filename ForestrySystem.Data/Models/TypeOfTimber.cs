using ForestrySystem.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ForestrySystem.Models
{
    public class TypeOfTimber
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Timber Name")]
        public TypeOfTimberEnum TimberName { get; set; }
        [DisplayName("Amount for Logging")]
        public float AmountForLogging { get; set; }
        [DisplayName("Year of Logging")]
        public DateTime YearOfLogging { get; set; }
    }
}
