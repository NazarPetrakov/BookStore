﻿using BookStore.Application.Abstract.Repositories;
using BookStore.Domain.Models.Category;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Category>> GetByIdsAsync(int[] ids)
        {
            return await _entities.Where(c => ids.Contains(c.Id)).ToListAsync();
        }
    }
}
