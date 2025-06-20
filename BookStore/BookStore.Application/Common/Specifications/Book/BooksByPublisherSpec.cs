namespace BookStore.Application.Common.Specifications.Book
{
    public class BooksByPublisherSpec : BookSpec
    {
        public BooksByPublisherSpec(int publisherId) : base(b => b.PublisherId == publisherId)
        {
        }
    }
}
