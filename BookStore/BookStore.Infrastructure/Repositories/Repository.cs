using BookStore.Application.Abstract;
using BookStore.Domain.Models;

namespace BookStore.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
    }
}
