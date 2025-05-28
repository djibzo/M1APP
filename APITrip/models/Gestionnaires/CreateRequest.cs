using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Gestionnaires
{
    public class CreateRequest
    {
        [Required]
        public string CNIGestionnaire { get; set; }
    }
}
