namespace BookStore.Application.Common.Specifications.Category
{
    public class CategoryOrderedByNameSpec : BaseSpecification<Domain.Models.Category.Category>
    {
        public CategoryOrderedByNameSpec() : base() 
        {
            AddOrderBy(c => c.Name);
        }
    }
    public class CategoryOrderedByIdSpec : BaseSpecification<Domain.Models.Category.Category>
    {
        public CategoryOrderedByIdSpec() : base()
        {
            AddOrderBy(c => c.Id);
        }
    }
}
