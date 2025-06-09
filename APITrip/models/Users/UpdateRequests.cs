namespace APITrip.models.users
{
    using System.ComponentModel.DataAnnotations;
    using APITrip.Entities;
    public class UpdateRequest
    {
        public required string Title { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [EnumDataType(typeof(Role))]
        public required string Role { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
    }
}
