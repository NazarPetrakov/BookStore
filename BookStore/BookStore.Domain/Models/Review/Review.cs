namespace BookStore.Domain.Models.Review
{
    public class Review : BaseEntity
    {
        public ushort Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        public string UserId { get; set; }
        public User.ApplicationUser User { get; set; } = null!;

        public int BookId { get; set; }
        public Book.Book Book { get; set; } = null!;
    }
}
