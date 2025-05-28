using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Chauffeurs
{
    public class ChauffeurCreateRequest
    {
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
    }
}
