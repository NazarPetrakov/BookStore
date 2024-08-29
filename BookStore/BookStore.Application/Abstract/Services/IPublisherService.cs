using BookStore.Domain.Common.Pagination;
using BookStore.Domain.Models.Category;
using BookStore.Domain.Models.Publisher;

namespace BookStore.Application.Abstract.Services
{
    public interface IPublisherService
    {
        Task<bool> CreateAsync(Publisher publisher);

        Task<IEnumerable<Publisher>> GetAllAsync();

        Task<Publisher> GetByIdAsync(int publisherId);
        Task<PagedList<Publisher>> GetPagedListAsync(PublisherParameters parameters);

        Task<bool> UpdateAsync(Publisher publisher);

        Task<bool> DeleteAsync(int publisherId);
    }
}
