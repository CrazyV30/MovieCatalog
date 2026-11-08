using System.ComponentModel.DataAnnotations;

namespace backEnd.Contracts
{
    public class CreateMovieRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 50 characters.")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 500 characters.")]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Url(ErrorMessage = "PhotoPath must be a valid URL.")]
        public string PhotoPath { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^0?[012]:[0-5][0-9]$", ErrorMessage = "Runtime must be in the format '0X:YY', where X and Y are numbers.")]
        public string Runtime { get; set; } = string.Empty;
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Director must be between 1 and 50 characters.")]
        public string Director { get; set; } = string.Empty;
        [Required]
        [Range(1800, 2026)]
        public int ReleaseYear { get; set; }
    }
}
