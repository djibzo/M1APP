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
        // treat empty string as null for password fields to
        // make them optional in front end apps
        private string _password;
        [MinLength(6)]
        public string Password
        {
            get => _password;
            set => _password = replaceEmptyWithNull(value);
        }
        private string _confirmPassword;
        [Compare("Password")]
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => _confirmPassword = replaceEmptyWithNull(value);
        }
        // helpers
        private string replaceEmptyWithNull(string value)
        {
            // replace empty string with null to make field optional
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
