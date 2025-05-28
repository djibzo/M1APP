using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Clients
{
    public class UpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CniClient { get; set; }
    }
}
