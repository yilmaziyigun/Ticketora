using Microsoft.AspNetCore.Identity;

namespace Ticketora.Persistence.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}
