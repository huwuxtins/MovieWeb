namespace MovieWeb.DTOs
{
    public class UserDTO
    {
        public string? Username { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? IsLocked { get; set; }
    }
}
