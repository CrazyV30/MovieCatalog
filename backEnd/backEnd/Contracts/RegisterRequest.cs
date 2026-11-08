using System.ComponentModel.DataAnnotations;

namespace backEnd.Contracts
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(20, MinimumLength =6)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        [StringLength(50,MinimumLength = 2)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Surname { get; set; } = string.Empty;
    }
}
