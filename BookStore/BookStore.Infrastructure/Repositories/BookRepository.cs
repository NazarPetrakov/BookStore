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

        public async Task<ICollection<Book>> GetBooksByPublisherIdAsync(int publisherId)
        {
            return await _entities.Where(b => b.PublisherId == publisherId)
                .ToListAsync();
        }

        public override async Task<Book?> GetByIdAsync(int id)
        {
            return await _entities.Where(b => b.Id == id)
                .Include(b => b.Publisher)
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync();
        }
        public override async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _entities.Include(b => b.Publisher).ToListAsync();
        }
    }
}
