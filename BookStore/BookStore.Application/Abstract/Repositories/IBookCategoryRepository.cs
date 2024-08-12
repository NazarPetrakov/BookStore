using BookStore.Domain.Models.JoinEntities;

namespace BookStore.Application.Abstract.Repositories
{
    public interface IBookCategoryRepository
    {
        Task<ICollection<BookCategory>> GetAllAsync();
        void RemoveRange(IEnumerable<BookCategory> entities);
        void AddRange(IEnumerable<BookCategory> entities);
    }
}
