﻿using BookStore.Application.Abstract;
using BookStore.Application.Abstract.Services;
using BookStore.Domain.Common.Pagination;
using BookStore.Domain.Exceptions;
using BookStore.Domain.Models.Publisher;

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
                throw new EntityNotFoundException("Publisher not found");

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
            var entities = await UnitOfWork.PublisherRepository.GetAllAsync();
            var query = entities.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                query = query.Where(p => p.Name.Contains(parameters.SearchTerm));
            }

            query = parameters.OrderBy?.ToLower() switch
            {
                "name" => query.OrderBy(p => p.Name),
                _ => query.OrderBy(p => p.Id)
            };

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
                throw new EntityNotFoundException("Publisher not found");

            return publisher;
        }

        public async Task<bool> UpdateAsync(Publisher publisher)
        {
            if (publisher == null)
                throw new ArgumentNullException(nameof(publisher));

            var entity = await UnitOfWork.PublisherRepository
                .GetByIdAsync(publisher.Id);

            if (entity == null)
                throw new EntityNotFoundException("Publisher not found");

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
