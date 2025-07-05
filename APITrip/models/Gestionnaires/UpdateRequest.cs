using APITrip.Entities;
using System.ComponentModel.DataAnnotations;

namespace APITrip.models.Gestionnaires
{
    public class GestionnaireUpdateRequest
    {
        public string CNIGestionnaire { get; set; }
        public required string Title { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [EnumDataType(typeof(Role))]
        public required string Role { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
    }
}
