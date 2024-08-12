using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BookStore.Application.Abstract
{
    public interface ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T, bool>>? Criteria { get; }
        List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; }
    }
}
