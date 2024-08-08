using BookStore.Domain.Models.JoinEntities;

namespace BookStore.Application.Abstract.Repositories
{
    public interface IBookAuthorRepository
    {
        Task<ICollection<BookAuthor>> GetAllAsync();
        void RemoveRange(IEnumerable<BookAuthor> entities);
        void AddRange(IEnumerable<BookAuthor> entities);
    }
}
