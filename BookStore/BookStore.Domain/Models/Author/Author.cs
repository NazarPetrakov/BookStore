namespace BookStore.Domain.Models.Author
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio {  get; set; }

        public ICollection<Book.Book> Books { get; } = new List<Book.Book>();

    }
}
