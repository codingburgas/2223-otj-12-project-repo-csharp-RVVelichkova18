using ForestrySystem.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ForestrySystem.Models
{
    public class PurposeOfCutOff
    {
        [Key]
        public int Id { get; set; }
        public PurposeOfCutOffsEnum Purpose { get; set; }
        [DisplayName("Percentage per Year")]
        public float PercentagePerYear { get; set; }
    }
}
