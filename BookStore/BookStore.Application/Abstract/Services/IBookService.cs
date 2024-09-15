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
        Task<PagedList<Book>> GetByAuthorPagedListAsync(int authorId, BookParameters parameters);
        Task<PagedList<Book>> GetByCategoryPagedListAsync(int categoryId, BookParameters parameters);
        Task<PagedList<Book>> GetByPublisherPagedListAsync(int publisherId, BookParameters parameters);

    }
}
