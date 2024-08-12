using BookStore.Application.Abstract;
using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Common.Specifications
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if(spec.Includes.Any()) 
            {
                query = spec.Includes.Aggregate(query, (current, include) => include(current));
            }

            return query;
        }
    }
}
