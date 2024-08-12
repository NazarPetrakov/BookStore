using BookStore.Application.Abstract.Repositories;
using BookStore.Domain.Models.JoinEntities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class BookAuthorRepository : IBookAuthorRepository
    {
        private readonly AppDbContext _context;
        public BookAuthorRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddRange(IEnumerable<BookAuthor> entities)
        {
            _context.BookAuthors.AddRange(entities);
        }

        public async Task<ICollection<BookAuthor>> GetAllAsync()
        {
            return await _context.BookAuthors.ToListAsync();
        }

        public void RemoveRange(IEnumerable<BookAuthor> entities)
        {
            _context.BookAuthors.RemoveRange(entities);
        }
    }
}
