namespace BookStore.Application.Common.Specifications.Book
{
    public class BooksByAuthorSpec : BookSpec<Domain.Models.Book.Book>
    {
        public BooksByAuthorSpec(int authorId) : base(x => x.BookAuthors.Any(ba => ba.AuthorId == authorId))
        {
        }
    }
}
