namespace BookStore.Domain.Models.Category
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Book.Book> Books { get; } = new List<Book.Book>();

    }
}
