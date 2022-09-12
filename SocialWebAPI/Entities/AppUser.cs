using System.ComponentModel.DataAnnotations;

namespace SocialWebAPI.Entities
{
    public class AppUser
    {
        [Key]
        public int AppUserId{ get; set; }
        public string UserName{ get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}
