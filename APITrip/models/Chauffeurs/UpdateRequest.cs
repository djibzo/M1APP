using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Chauffeurs
{
    public class UpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
    }
}
