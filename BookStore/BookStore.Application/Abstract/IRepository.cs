using BookStore.Domain.Models;
using System.Linq.Expressions;

namespace BookStore.Application.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate);
    }
}
