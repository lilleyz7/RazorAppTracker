using Microsoft.AspNetCore.Identity;

namespace AppTrackV2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Application> applications = new List<Application>();
    }
}
