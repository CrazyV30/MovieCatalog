namespace Domain.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;
        public string MoviePath { get; set; } = string.Empty;
        public string Runtime { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public List<MovieComment> Comments { get; set; } = new List<MovieComment>();
    }
}
