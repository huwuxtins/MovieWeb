using MovieWeb.Models;

namespace MovieWeb.DTOs
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Guid FilmId { get; set; }
    }
}
