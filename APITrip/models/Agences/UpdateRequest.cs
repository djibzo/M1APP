using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Agences
{
    public class AgenceUpdateRequest
    {
        [Required]
        public int IdAgence { get; set; }
        [Required]
        public string AdresseAgence { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        [Required]
        public string NineaGestionnaire { get; set; }
        [Required]
        public string RccmGestionnaire { get; set; }
        public int? IdGestionnaire { get; set; }
    }
}
