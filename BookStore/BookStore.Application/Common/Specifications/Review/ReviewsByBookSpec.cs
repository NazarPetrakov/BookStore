using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Common.Specifications.Review
{
    public class ReviewsByBookSpec : BaseSpecification<Domain.Models.Review.Review>
    {
        public ReviewsByBookSpec(int bookId) : base(r => r.BookId == bookId)
        {
            AddInclude(r => r.Include(r => r.Book));
            AddInclude(r => r.Include(r => r.User));
        }
    }
}
