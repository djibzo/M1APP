using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Clients
{
    public class CreateRequest
    {
        [Required]
        public string CniClient { get; set; }
    }
}
