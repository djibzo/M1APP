using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using APITrip.Entities;

namespace APITrip.models.Gestionnaires
{
    public class GestionnaireCreateRequest
    {
        [Required]
        public string CNIGestionnaire { get; set; }
        public string Title { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, MaxLength(80), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [EnumDataType(typeof(Role))]
        public string Role { get; set; }
        public string PasswordHash { get; set; }
    }
}
