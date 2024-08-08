using BookStore.Domain.Models.Author;

namespace BookStore.Application.Abstract.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetByIdsAsync(int[] ids);
    }
}
