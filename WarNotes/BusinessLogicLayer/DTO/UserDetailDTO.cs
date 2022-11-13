using BusinessLogicLayer.Enums;

namespace BusinessLogicLayer.DTO
{
    public class UserDetailDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsBlocked { get; set; }
        public Role Role { get; set; }
    }
}
