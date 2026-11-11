using System.ComponentModel.DataAnnotations;

namespace backEnd.Contracts
{
    public class CreateMovieRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;
        public string MoviePath { get; set; } = string.Empty;
        public string Runtime { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
    }
}
