using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Clients
{
    public class ClientCreateRequest
    {
        [Required]
        public string CniClient { get; set; }
    }
}
