namespace BookStore.Application.Common.Specifications.Book
{
    public class BookOrderedByTitleSpec : BookSpec
    {
        public BookOrderedByTitleSpec() : base()
        {
            AddOrderBy(b => b.Title);
        }
    }
    public class BookOrderedByPriceSpec : BookSpec
    {
        public BookOrderedByPriceSpec() : base()
        {
            AddOrderBy(b => b.Price);
        }
    }
    public class BookOrderedByPublicationYearSpec : BookSpec
    {
        public BookOrderedByPublicationYearSpec() : base()
        {
            AddOrderBy(b => b.PublicationYear);
        }
    }
}
