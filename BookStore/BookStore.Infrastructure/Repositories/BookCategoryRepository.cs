using BookStore.Application.Abstract.Repositories;
using BookStore.Domain.Models.JoinEntities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class BookCategoryRepository : IBookCategoryRepository
    {
        private readonly AppDbContext _context;
        public BookCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddRange(IEnumerable<BookCategory> entities)
        {
            _context.BookCategories.AddRange(entities);
        }

        public async Task<ICollection<BookCategory>> GetAllAsync()
        {
            return await _context.BookCategories.ToListAsync();   
        }

        public void RemoveRange(IEnumerable<BookCategory> entities)
        {
            _context.BookCategories.RemoveRange(entities);
        }
    }
}
