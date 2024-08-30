using BookStore.Domain.Common.Pagination;
using BookStore.Domain.Models.Category;

namespace BookStore.Application.Abstract.Services
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(Category category);

        Task<IEnumerable<Category>> GetAllAsync();
        Task<PagedList<Category>> GetPagedListAsync(CategoryParameters parameters);

        Task<Category> GetByIdAsync(int categoryId);

        Task<bool> UpdateAsync(Category category);

        Task<bool> DeleteAsync(int categoryId);
    }
}
