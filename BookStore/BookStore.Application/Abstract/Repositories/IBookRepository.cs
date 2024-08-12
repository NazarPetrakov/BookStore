using BookStore.Domain.Models.Book;

namespace BookStore.Application.Abstract.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
    }
}
