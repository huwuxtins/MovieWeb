using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWeb.Models
{
    public class CommentModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public UserModel User { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        [ForeignKey("Film")]
        public Guid FilmId { get; set; }
        public FilmModel Film { get; set; }

        public CommentModel() { }
    }
}
