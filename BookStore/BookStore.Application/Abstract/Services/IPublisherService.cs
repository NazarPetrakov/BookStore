using BookStore.Domain.Models.Book;
using BookStore.Domain.Models.Publisher;

namespace BookStore.Application.Abstract.Services
{
    public interface IPublisherService
    {
        Task<bool> CreateAsync(Publisher publisher);

        Task<IEnumerable<Publisher>> GetAllAsync();

        Task<Publisher> GetByIdAsync(int publisherId);

        Task<bool> UpdateAsync(Publisher publisher);

        Task<bool> DeleteAsync(int publisherId);
        Task<IEnumerable<Book>> GetBooksByPublisher(int publisherId);
    }
}
