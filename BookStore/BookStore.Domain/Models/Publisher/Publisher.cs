namespace BookStore.Domain.Models.Publisher
{
    public class Publisher : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<Book.Book> Books { get; } = new List<Book.Book>();
    }
}
