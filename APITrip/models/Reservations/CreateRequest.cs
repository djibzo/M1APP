using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Reservations
{
    public class CreateRequest
    {
        [Required]
        public DateTime DateReservation { get; set; }
        [Required]
        public float MontantReservation { get; set; }
        [Required]
        public string StatutReservation { get; set; }
        [Required]
        public int ClientId { get; set; }
    }
}
