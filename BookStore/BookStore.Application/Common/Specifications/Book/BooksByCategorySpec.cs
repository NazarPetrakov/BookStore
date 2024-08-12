namespace BookStore.Application.Common.Specifications.Book
{
    public class BooksByCategorySpec : BookSpec<Domain.Models.Book.Book>
    {
        public BooksByCategorySpec(int categoryId) : base(x => x.BookCategories.Any(bc => bc.CategoryId == categoryId))
        {
        }
    }
}
