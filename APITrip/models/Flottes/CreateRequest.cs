namespace APITrip.models.Flotte
{
    using System.ComponentModel.DataAnnotations;
    using APITrip.Entities;
    public class FlotteCreateRequest
    {
        [Required]
        public int IdFlotte { get; set; }
        [Required]
        public required string TypeFlotte { get; set; }
        [Required]
        public required string MatriculeFlotte { get; set; }
    }
}
