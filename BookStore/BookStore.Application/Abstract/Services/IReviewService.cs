using BookStore.Domain.Common.Pagination;
using BookStore.Domain.Models.Publisher;
using BookStore.Domain.Models.Review;

namespace BookStore.Application.Abstract.Services
{
    public interface IReviewService
    {
        Task<bool> CreateAsync(Review review);

        Task<IEnumerable<Review>> GetByBookAsync(int bookId);

        Task<Review> GetByIdAsync(int reviewId);
        Task<PagedList<Review>> GetPagedListAsync(ReviewParameters parameters);

        Task<bool> UpdateAsync(Review review);

        Task<bool> DeleteAsync(int reviewId);
    }
}
