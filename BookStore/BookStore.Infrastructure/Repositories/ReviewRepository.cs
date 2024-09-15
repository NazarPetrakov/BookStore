using BookStore.Application.Abstract.Repositories;
using BookStore.Domain.Models.Review;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context)
        {
        }
    }
}
