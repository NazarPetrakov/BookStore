using BookStore.Domain.Models.Author;
using BookStore.Domain.Models.Book;
using BookStore.Domain.Models.Review;

namespace BookStore.Application.Abstract.Services
{
    public interface IBookService
    {
        Task<bool> CreateAsync(Book book, int[] categoryIds, int[] authorIds, int? publisherId);

        Task<IEnumerable<Book>> GetAllAsync();

        Task<Book> GetByIdAsync(int bookId);

        Task<bool> UpdateAsync(Book book, int[]? categoryIds, int[]? authorIds, int? publisherId);

        Task<bool> DeleteAsync(int bookId);
        Task<IEnumerable<Review>> GetBookReviews(int bookId);
    }
}
