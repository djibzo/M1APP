using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Clients
{
    public class ClientUpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CniClient { get; set; }
    }
}
