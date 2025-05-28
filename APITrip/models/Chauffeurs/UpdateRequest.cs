using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Chauffeurs
{
    public class ChauffeurUpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
    }
}
