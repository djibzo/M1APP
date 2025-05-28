using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Offres
{
    public class CreateRequest
    {
        [Required]
        public string DescriptionOffre { get; set; }
        [Required]
        public float PrixOffre { get; set; }
        public string DisponibiliteOffre { get; set; }
        [Required]
        public int IdAgence { get; set; }
    }
}
