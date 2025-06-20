using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Common.Specifications.Review
{
    public class DetailedReviewSpec : BaseSpecification<Domain.Models.Review.Review>
    {
        public DetailedReviewSpec() : base()
        {
            AddInclude(r => r.Include(r => r.Book));
            AddInclude(r => r.Include(r => r.User));

        }
    }
}
