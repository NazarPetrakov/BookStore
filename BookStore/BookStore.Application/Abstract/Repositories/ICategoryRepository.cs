﻿using BookStore.Domain.Common.Pagination;
using BookStore.Domain.Models.Book;
using BookStore.Domain.Models.Category;

namespace BookStore.Application.Abstract.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetByIdsAsync(int[] ids);
    }
}
