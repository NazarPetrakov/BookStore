using Microsoft.AspNetCore.Identity;

namespace BookStore.Domain.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
