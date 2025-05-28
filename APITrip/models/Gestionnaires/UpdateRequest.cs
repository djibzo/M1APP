using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Gestionnaires
{
    public class GestionnaireUpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CNIGestionnaire { get; set; }
    }
}
