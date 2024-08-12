namespace BookStore.Application.Common.Specifications.Book
{
    public class DetailedBookSpec : BookSpec<Domain.Models.Book.Book>
    {
        public DetailedBookSpec() : base()
        {
        }
        public DetailedBookSpec(int bookId) : base(b => b.Id == bookId)
        {
        }
    }
}
