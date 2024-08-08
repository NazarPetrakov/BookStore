namespace BookStore.Application.Contracts.Book
{
    public class GetBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateOnly PublicationYear { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
    }
}
