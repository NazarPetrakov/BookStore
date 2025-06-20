﻿using BookStore.Application.Abstract;
using BookStore.Application.Abstract.Services;
using BookStore.Domain.Common.Pagination;
using BookStore.Domain.Exceptions;
using BookStore.Domain.Models.Book;
using BookStore.Domain.Models.Category;

namespace BookStore.Application.Services
{
    public class CategoryService : ICategoryService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public CategoryService(IUnitOfWork unitOfWork)
        {

            UnitOfWork = unitOfWork;

        }
        public async Task<bool> CreateAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            await UnitOfWork.CategoryRepository.AddAsync(category);

            return SaveChangesAndCheckResult();
        }

        public async Task<bool> DeleteAsync(int categoryId)
        {
            var entity = await UnitOfWork.CategoryRepository.GetByIdAsync(categoryId);

            if (entity == null)
                throw new EntityNotFoundException("Category not found");

            UnitOfWork.CategoryRepository.Delete(entity);

            return SaveChangesAndCheckResult();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var entities = await UnitOfWork.CategoryRepository.GetAllAsync();

            return entities;
        }
        public async Task<PagedList<Category>> GetPagedListAsync(CategoryParameters parameters)
        {
            var entities = await UnitOfWork.CategoryRepository.GetAllAsync();
            var query = entities.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                query = query.Where(c => c.Name.Contains(parameters.SearchTerm));
            }

            query = parameters.OrderBy?.ToLower() switch
            {
                "name" => query.OrderBy(c => c.Name),
                _ => query.OrderBy(c => c.Id)
            };

            var result = PagedList<Category>.ToPagedList(query,
                parameters.PageNumber,
                parameters.PageSize);

            return result;
        }

        public async Task<Category> GetByIdAsync(int categoryId)
        {
            var entity = await UnitOfWork.CategoryRepository.GetByIdAsync(categoryId);

            if (entity == null)
                throw new EntityNotFoundException("Category not found");

            return entity;
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            var entity = await UnitOfWork.CategoryRepository
                .GetByIdAsync(category.Id);

            if (entity == null)
                throw new EntityNotFoundException("Category not found");

            entity.Name = category.Name ?? entity.Name;

            UnitOfWork.CategoryRepository.Update(entity);

            return SaveChangesAndCheckResult();
        }
        private bool SaveChangesAndCheckResult()
        {
            var result = UnitOfWork.Save();
            return result > 0;
        }

        
    }
}
