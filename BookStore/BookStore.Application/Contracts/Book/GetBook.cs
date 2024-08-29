using BookStore.Application.Contracts.Author;
using BookStore.Application.Contracts.Category;
using BookStore.Application.Contracts.Publisher;

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

        public GetPublisher? Publisher { get; set; }
        public ICollection<GetAuthor> Authors { get; set; } = new List<GetAuthor>();
        public ICollection<GetCategory> Categories { get; set; } = new List<GetCategory>();
    }
}
