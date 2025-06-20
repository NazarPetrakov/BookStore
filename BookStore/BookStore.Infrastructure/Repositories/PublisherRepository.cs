using BookStore.Application.Abstract.Repositories;
using BookStore.Domain.Models.Publisher;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(AppDbContext context) : base(context)
        {
        }
    }
}
