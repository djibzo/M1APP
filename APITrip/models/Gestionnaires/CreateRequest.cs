using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Gestionnaires
{
    public class GestionnaireCreateRequest
    {
        [Required]
        public string CNIGestionnaire { get; set; }
    }
}
