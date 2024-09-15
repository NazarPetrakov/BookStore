using BookStore.Domain.Common.Pagination;
using BookStore.Domain.Models.Review;

namespace BookStore.Application.Abstract.Services
{
    public interface IReviewService
    {
        Task<PagedList<Review>> GetPagedListAsync(ReviewParameters parameters);
        Task<PagedList<Review>> GetByBookPagedListAsync(int bookId, ReviewParameters parameters);
        Task<Review> GetByIdAsync(int reviewId);
        Task<bool> CreateAsync(Review review, int bookId, string userId);
        Task<bool> DeleteAsync(int reviewId);
    }
}
