using BookStore.Application.Abstract.Repositories;
using BookStore.Domain.Models.Author;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }
        public override async Task<Author?> GetByIdAsync(int id)
        {
            return await _entities.Where(a => a.Id == id)
                .Include(a => a.BookAuthors).ThenInclude(ba => ba.Book)
                .FirstOrDefaultAsync();
        }
    }
}
