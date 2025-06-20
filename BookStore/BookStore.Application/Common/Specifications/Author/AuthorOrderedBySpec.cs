namespace BookStore.Application.Common.Specifications.Author
{
    public class AuthorOrderedByFirstNameSpec : BaseSpecification<Domain.Models.Author.Author>
    {
        public AuthorOrderedByFirstNameSpec()
        {
            AddOrderBy(a => a.FirstName);
        }
    }
    public class AuthorOrderedByLastNameSpec : BaseSpecification<Domain.Models.Author.Author>
    {
        public AuthorOrderedByLastNameSpec()
        {
            AddOrderBy(a => a.LastName);
        }
    }
    public class AuthorOrderedByIdSpec : BaseSpecification<Domain.Models.Author.Author>
    {
        public AuthorOrderedByIdSpec()
        {
            AddOrderBy(a => a.Id);
        }
    }
}
