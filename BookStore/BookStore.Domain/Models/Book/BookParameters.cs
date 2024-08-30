using BookStore.Domain.Common.Pagination;

namespace BookStore.Domain.Models.Book
{
    public class BookParameters : QueryStringParameters
    {
        public uint MinPrice { get; set; } = 1;
        public uint MaxPrice { get; set; } = 1000;
        public DateTime MinPublicationYear { get; set; } = new DateTime(1, 1, 1);
        public DateTime MaxPublicationYear { get; set; } = DateTime.Today;
    }
}
