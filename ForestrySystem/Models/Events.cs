using ForestrySystem.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForestrySystem.Models
{
    public class Events
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Date/Time")]
        public DateTime Date { get; set; }
        public EventsEnum Status { get; set; }
        public string Purpose { get; set; }

        [ForeignKey("ForestryInstitution")]
        [DisplayName("Institution ID")]
        public int FIEventRefID { get; set; }
        public ForestryInstitution? Institutions { get; set; }

    }
}
