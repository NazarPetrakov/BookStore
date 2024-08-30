namespace BookStore.Application.Common.Specifications.Publisher
{
    public class PublisherOrderedByNameSpec : BaseSpecification<Domain.Models.Publisher.Publisher>
    {
        public PublisherOrderedByNameSpec()
        {
            AddOrderBy(p => p.Name);
        }
    }
    public class PublisherOrderedByIdSpec : BaseSpecification<Domain.Models.Publisher.Publisher>
    {
        public PublisherOrderedByIdSpec()
        {
            AddOrderBy(p => p.Id);
        }
    }
}
