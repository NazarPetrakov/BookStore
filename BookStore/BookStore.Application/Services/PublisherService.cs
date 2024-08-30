using BookStore.Application.Abstract;
using BookStore.Application.Abstract.Services;
using BookStore.Application.Common.Specifications;
using BookStore.Domain.Common.Pagination;
using BookStore.Domain.Exceptions;
using BookStore.Domain.Models.Book;
using BookStore.Domain.Models.Category;
using BookStore.Domain.Models.Publisher;
using BookStore.Application.Common.Specifications.Publisher;

namespace BookStore.Application.Services
{
    public class PublisherService : IPublisherService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public PublisherService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public async Task<bool> CreateAsync(Publisher publisher)
        {
            if (publisher == null)
                throw new ArgumentNullException(nameof(publisher));

            await UnitOfWork.PublisherRepository.AddAsync(publisher);

            return SaveChangesAndCheckResult();
        }

        public async Task<bool> DeleteAsync(int publisherId)
        {
            var publisher = await UnitOfWork.PublisherRepository
                .GetByIdAsync(publisherId);

            if (publisher == null)
                throw new EntityNotFoundException("Publissher not found");

            UnitOfWork.PublisherRepository.Delete(publisher);

            return SaveChangesAndCheckResult();
        }

        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            var publisherList = await UnitOfWork.PublisherRepository.GetAllAsync();

            return publisherList;
        }
        public async Task<PagedList<Publisher>> GetPagedListAsync(PublisherParameters parameters)
        {
            BaseSpecification<Publisher> spec = new PublisherOrderedByIdSpec();

            if (!string.IsNullOrEmpty(parameters.OrderBy))
            {
                spec = parameters.OrderBy.ToLower() switch
                {
                    "name" => new PublisherOrderedByNameSpec(),
                    _ => new PublisherOrderedByIdSpec()
                };
            }

            var entities = await UnitOfWork.PublisherRepository.GetAsync(spec);
            var query = entities.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                query = query.Where(c => c.Name.Contains(parameters.SearchTerm));
            }

            var result = PagedList<Publisher>.ToPagedList(query,
                parameters.PageNumber,
                parameters.PageSize);

            return result;
        }
        public async Task<Publisher> GetByIdAsync(int publisherId)
        {
            var publisher = await UnitOfWork.PublisherRepository
                .GetByIdAsync(publisherId);

            if (publisher == null)
                throw new EntityNotFoundException("Publissher not found");

            return publisher;
        }

        public async Task<bool> UpdateAsync(Publisher publisher)
        {
            if (publisher == null)
                throw new ArgumentNullException(nameof(publisher));

            var entity = await UnitOfWork.PublisherRepository
                .GetByIdAsync(publisher.Id);

            if (entity == null)
                throw new EntityNotFoundException("Publissher not found");

            entity.Name = publisher.Name ?? entity.Name;
            entity.Address = publisher.Address ?? entity.Address;

            UnitOfWork.PublisherRepository.Update(entity);

            return SaveChangesAndCheckResult();
        }
        private bool SaveChangesAndCheckResult()
        {
            var result = UnitOfWork.Save();
            return result > 0;
        }

        
    }
}
