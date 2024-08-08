using BookStore.Application.Abstract.Repositories;
using BookStore.Domain.Models.Book;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }
        public override async Task<Book?> GetByIdAsync(int id)
        {
            return await _entities.Where(b => b.Id == id)
                //.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                //.Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
                //.Include(b => b.BookUsers).ThenInclude(bu => bu.User)
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync();
        }
    }
}
