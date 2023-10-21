using Microsoft.AspNetCore.Identity;

namespace SocialWebAPI.Entities;

public class AppRole : IdentityRole<int>
{
    public ICollection<AppUserRole> UserRoles { get; set; }
}
