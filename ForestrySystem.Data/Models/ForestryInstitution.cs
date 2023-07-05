using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ForestrySystem.Models
{
    public class ForestryInstitution
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        [DisplayName("Total Area")]
        public float TotalArea { get; set; }
        [DisplayName("Green Area")]
        public float GreenArea { get; set; }
        [DisplayName("Urbanized Area")]
        public float UrbanizedArea { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public ICollection<Events>? Events { get; set; }
    }
}
