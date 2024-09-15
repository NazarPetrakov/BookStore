using BookStore.Application.Abstract;
using BookStore.Application.Abstract.Services;
using BookStore.Application.Common.Specifications.Review;
using BookStore.Domain.Common.Pagination;
using BookStore.Domain.Exceptions;
using BookStore.Domain.Models.Review;

namespace BookStore.Application.Services
{
    public class ReviewService : IReviewService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        private readonly IAuthenticateService _authenticateService;
        public ReviewService(IUnitOfWork unitOfWork, IAuthenticateService authService)
        {
            UnitOfWork = unitOfWork;
            _authenticateService = authService;
        }
        public async Task<PagedList<Review>> GetByBookPagedListAsync(int bookId,
            ReviewParameters parameters)
        {
            var spec = new ReviewsByBookSpec(bookId);
            var entities = await UnitOfWork.ReviewRepository.GetAsync(spec);

            return GetPagedReviewsAsync(entities, parameters);
        }

        public async Task<Review> GetByIdAsync(int reviewId)
        {
            var review = await UnitOfWork.ReviewRepository.GetByIdAsync(reviewId);

            if (review == null)
                throw new EntityNotFoundException("Review not found");

            return review;
        }

        public async Task<PagedList<Review>> GetPagedListAsync(ReviewParameters parameters)
        {
            var spec = new DetailedReviewSpec();
            var entities = await UnitOfWork.ReviewRepository.GetAsync(spec);

            return GetPagedReviewsAsync(entities, parameters);
        }
        public async Task<bool> CreateAsync(Review review, int bookId, string userId)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            var book = await UnitOfWork.BookRepository.GetByIdAsync(bookId);
            var user = await _authenticateService.GetUserByIdAsync(userId);

            if (book == null)
                throw new EntityNotFoundException("Book not found");

            review.Book = book;
            review.BookId = bookId;

            review.UserId = userId;
            review.User = user;

            review.ReviewDate = DateTime.Now;

            await UnitOfWork.ReviewRepository.AddAsync(review);

            return SaveChangesAndCheckResult();
        }

        public async Task<bool> DeleteAsync(int reviewId)
        {
            var review = await UnitOfWork.ReviewRepository.GetByIdAsync(reviewId);

            if (review == null)
                throw new EntityNotFoundException("Review not found");

            UnitOfWork.ReviewRepository.Delete(review);

            return SaveChangesAndCheckResult();
        }

        private PagedList<Review> GetPagedReviewsAsync(IEnumerable<Review> entities, ReviewParameters parameters)
        {
            var query = entities.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                query = query.Where(r => r.Rating == Convert.ToUInt16(parameters.SearchTerm));
            }

            query = parameters.OrderBy?.ToLower() switch
            {
                "rating" => query.OrderBy(r => r.Rating),
                "commentlength" or "comment" or "length" => query.OrderBy(r => r.Comment.Length),
                _ => query.OrderBy(r => r.Id)
            };

            return PagedList<Review>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
        private bool SaveChangesAndCheckResult()
        {
            var result = UnitOfWork.Save();
            return result > 0;
        }
    }
}
