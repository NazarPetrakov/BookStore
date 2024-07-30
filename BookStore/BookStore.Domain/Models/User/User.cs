using Microsoft.AspNetCore.Identity;

namespace BookStore.Domain.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public ICollection<Review.Review> Reviews { get; } = new List<Review.Review>();

        public ICollection<Book.Book> Books { get; } = new List<Book.Book>();

    }
}
