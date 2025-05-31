using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Agences
{
    public class AgenceCreateRequest
    {
        [Required]
        public required string AdresseAgence { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        [Required]
        public required string NineaGestionnaire { get; set; }
        [Required]
        public required string RccmGestionnaire { get; set; }
        public int? IdGestionnaire { get; set; }
    }
}
