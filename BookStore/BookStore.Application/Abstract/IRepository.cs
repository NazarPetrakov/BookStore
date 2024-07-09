using BookStore.Domain.Models;

namespace BookStore.Application.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
    }
}
