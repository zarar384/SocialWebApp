using System.ComponentModel.DataAnnotations;

namespace SocialWebAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(30)]
        public string Username { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }

    }
}
