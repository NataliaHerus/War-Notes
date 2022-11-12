using BusinessLogicLayer.Enums;

namespace BusinessLogicLayer.Services.DTO
{
    public class UserRegistrationDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Role Role { get; set; }
    }
}
