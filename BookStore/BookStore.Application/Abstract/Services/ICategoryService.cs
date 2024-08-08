using BookStore.Domain.Models.Author;
using BookStore.Domain.Models.Category;

namespace BookStore.Application.Abstract.Services
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(Category category);

        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category> GetByIdAsync(int categoryId);

        Task<bool> UpdateAsync(Category category);

        Task<bool> DeleteAsync(int categoryId);
        Task<IEnumerable<Category>> GetBooksByCategory(int categoryId);
    }
}
