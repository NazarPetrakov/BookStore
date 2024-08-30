namespace BookStore.Application.Common.Specifications.Book
{
    public class DetailedBookSpec : BookSpec
    {
        public DetailedBookSpec() : base()
        {
            AddOrderBy(b => b.Id);
        }
        public DetailedBookSpec(int bookId) : base(b => b.Id == bookId)
        {
            AddOrderBy(b => b.Id);
        }
    }
}
