namespace BookStore.Domain.Models.JoinEntities
{
    public class BookUser : BaseEntity
    {
        public int? BookId { get; set; }
        public Book.Book? Book { get; set; }
        public string? UserId { get; set; }
        public User.ApplicationUser? User { get; set; }
    }
}
