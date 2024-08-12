using BookStore.Domain.Models.Author;
using BookStore.Domain.Models.Book;

namespace BookStore.Application.Abstract.Services
{
    public interface IAuthorService
    {
        Task<bool> CreateAsync(Author author);

        Task<IEnumerable<Author>> GetAllAsync();

        Task<Author> GetByIdAsync(int authorId);

        Task<bool> UpdateAsync(Author author);

        Task<bool> DeleteAsync(int authorId);
    }
}
