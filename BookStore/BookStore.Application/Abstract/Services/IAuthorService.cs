using BookStore.Domain.Common.Pagination;
using BookStore.Domain.Models.Author;
using BookStore.Domain.Models.Book;
using BookStore.Domain.Models.Category;

namespace BookStore.Application.Abstract.Services
{
    public interface IAuthorService
    {
        Task<bool> CreateAsync(Author author);

        Task<IEnumerable<Author>> GetAllAsync();
        Task<PagedList<Author>> GetPagedListAsync(AuthorParameters parameters);

        Task<Author> GetByIdAsync(int authorId);

        Task<bool> UpdateAsync(Author author);

        Task<bool> DeleteAsync(int authorId);
    }
}
