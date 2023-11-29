namespace MovieWeb.DTOs
{
    public class FilmDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Country { get; set; }
        public string Author { get; set; }
        public DateTime YearRelease { get; set; }
        public string ImageLink { get; set; }
        public string LinkUrl { get; set; }
        public string LinkDemo { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
        public int Reviews { get; set; }
        public int FilmLength { get; set; }

    }
}
