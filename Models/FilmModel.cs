using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWeb.Models
{
    public class FilmModel
    {
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Genre { get; set; }

        public string imageLink { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Author { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Country { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public DateTime YearRelease { get; set; }

        [Column(TypeName = "float")]
        public float Rating { get; set; }

        [Column(TypeName = "int")]
        public int Reviews { get; set; }

        public string LinkUrl { get; set; }

        public string LinkDemo { get; set; }

        public int FilmLength { get; set; }

        public FilmModel()
        {
            Rating = 0;
            Reviews = 0;
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
