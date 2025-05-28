using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Gestionnaires
{
    public class UpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CNIGestionnaire { get; set; }
    }
}
