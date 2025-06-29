﻿using BookStore.Application.Abstract.Repositories;
using BookStore.Domain.Models.Author;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Author>> GetByIdsAsync(int[] ids)
        {
            return await _entities.Where(a => ids.Contains(a.Id)).ToListAsync();
        }
    }
}
