using BookStore.Domain.Models;

namespace BookStore.Application.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAsync(ISpecification<T> spec);
        Task<T?> GetEntityWithSpec(ISpecification<T> spec);
        Task AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
