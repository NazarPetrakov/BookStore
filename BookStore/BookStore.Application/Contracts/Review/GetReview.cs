using BookStore.Application.Contracts.Book;
using BookStore.Application.Contracts.User;
using BookStore.Domain.Models.User;

namespace BookStore.Application.Contracts.Review
{
    public class GetReview
    {
        public int Id { get; set; }
        public ushort Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        public GetUser User { get; set; }
        public GetBook Book { get; set; }
    }
}
