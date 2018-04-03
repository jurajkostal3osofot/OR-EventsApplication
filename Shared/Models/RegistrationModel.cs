using Shared.Enums;

namespace Shared.Models
{
    public class RegistrationModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public LoginType Type { get; set; }
    }
}
