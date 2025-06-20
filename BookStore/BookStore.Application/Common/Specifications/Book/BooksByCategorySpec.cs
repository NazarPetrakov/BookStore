namespace BookStore.Application.Common.Specifications.Book
{
    public class BooksByCategorySpec : BookSpec
    {
        public BooksByCategorySpec(int categoryId) : base(x => x.BookCategories.Any(bc => bc.CategoryId == categoryId))
        {
        }
    }
}
