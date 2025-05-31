using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Voyages
{
    public class VoyageCreateRequest
    {
        [Required]
        public string Destination { get; set; }
        [Required]
        public DateTime DateDepart { get; set; }
        [Required]
        public DateTime DateArrivee { get; set; }
        [Required]
        public float Prix { get; set; }
    }
}
