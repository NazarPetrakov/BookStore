namespace BookStore.Domain.Models.Book
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationYear { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }

        public int? PublisherId { get; set; }
        public Publisher.Publisher? Publisher { get; set; }

        public ICollection<Review.Review> Reviews { get;} = new List<Review.Review>();
        public ICollection<JoinEntities.BookAuthor> BookAuthors { get; set; } = new List<JoinEntities.BookAuthor>();
        public ICollection<JoinEntities.BookCategory> BookCategories { get; set; } = new List<JoinEntities.BookCategory>();
        public ICollection<JoinEntities.BookUser> BookUsers { get; set; } = new List<JoinEntities.BookUser>();

    }
}
