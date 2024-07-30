namespace BookStore.Domain.Models.Book
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationYear { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public int? PublisherId { get; set; }
        public Publisher.Publisher? Publisher { get; set; }

        public ICollection<Review.Review> Reviews { get;} = new List<Review.Review>();

        public ICollection<Author.Author> Authors { get; } = new List<Author.Author>();

        public ICollection<Category.Category> Categories { get; } = new List<Category.Category>();

        public ICollection<User.ApplicationUser> Users { get; } = new List<User.ApplicationUser>();
    }
}
