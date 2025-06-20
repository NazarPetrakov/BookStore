namespace BookStore.Application.Common.Specifications.Book
{
    public class BooksByAuthorSpec : BookSpec
    {
        public BooksByAuthorSpec(int authorId) : base(x => x.BookAuthors.Any(ba => ba.AuthorId == authorId))
        {
        }
    }
}
