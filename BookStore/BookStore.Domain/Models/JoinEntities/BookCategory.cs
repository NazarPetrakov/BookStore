namespace BookStore.Domain.Models.JoinEntities
{
    public class BookCategory
    {
        public int? BookId { get; set; }
        public Book.Book? Book { get; set; }
        public int? CategoryId { get; set; }
        public Category.Category? Category { get; set; }
    }
}
