using BookStore.Application.Abstract.Repositories;
using BookStore.Domain.Models.Publisher;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Publisher?> GetByIdAsync(int id)
        {
            return await _entities.Where(e => e.Id == id)
                .Include(x => x.Books).FirstOrDefaultAsync();
        }
    }
}
