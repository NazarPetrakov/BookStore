namespace BookStore.Domain.Models.JoinEntities
{
    public class BookAuthor
    {
        public int? BookId { get; set; }
        public Book.Book? Book { get; set; }
        public int? AuthorId { get; set; }
        public Author.Author? Author { get; set; }
    }
}
