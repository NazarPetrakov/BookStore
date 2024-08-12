using BookStore.Application.Abstract;
using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BookStore.Application.Common.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public BaseSpecification()
        {
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>>? Criteria { get; }

        public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; }
            = new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>();
        protected void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
