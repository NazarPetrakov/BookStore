namespace BookStore.Application.Common.Specifications.Book
{
    public class BooksByPublisherSpec : BookSpec<Domain.Models.Book.Book>
    {
        public BooksByPublisherSpec(int publisherId) : base(b => b.PublisherId == publisherId)
        {
        }
    }
}
