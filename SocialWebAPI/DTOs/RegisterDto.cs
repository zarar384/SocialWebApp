using System.ComponentModel.DataAnnotations;

namespace SocialWebAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }

    }
}
