using BookStore.Domain.Common.Pagination;
using BookStore.Domain.Models.Book;

namespace BookStore.Application.Abstract.Services
{
    public interface IBookService
    {
        Task<bool> CreateAsync(Book book, int[] categoryIds, int[] authorIds, int? publisherId);

        Task<IEnumerable<Book>> GetAllAsync();
        Task<PagedList<Book>> GetPagedListAsync(BookParameters parameters);

        Task<Book> GetByIdAsync(int bookId);

        Task<bool> UpdateAsync(Book book, int[]? categoryIds, int[]? authorIds, int? publisherId);

        Task<bool> DeleteAsync(int bookId);
        Task<IEnumerable<Book>> GetByAuthorAsync(int authorId);
        Task<IEnumerable<Book>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<Book>> GetByPublisherAsync(int publisherId);

    }
}
