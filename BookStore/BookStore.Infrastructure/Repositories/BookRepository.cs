using BookStore.Application.Abstract.Repositories;
using BookStore.Domain.Models.Book;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }
    }
}
