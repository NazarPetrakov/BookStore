namespace BookStore.Domain.Models.Book
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationYear { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }


    }
}
