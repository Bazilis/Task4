using Microsoft.AspNetCore.Identity;

namespace WebApp.Data
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime RegistrationTime { get; set; } = DateTime.Now;

        public DateTime LastLoginTime { get; set; } = DateTime.Now;

        public bool IsBlocked { get; set; }
    }
}
