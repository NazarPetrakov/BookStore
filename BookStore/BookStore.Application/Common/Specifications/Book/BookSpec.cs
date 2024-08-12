using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.Application.Common.Specifications.Book
{
    public abstract class BookSpec<T> : BaseSpecification<T> where T : Domain.Models.Book.Book
    {
        protected BookSpec (Expression<Func<T, bool>> criteria) : base(criteria)
        {
            AddCommonIncludes();
        }
        protected BookSpec()
        {
            AddCommonIncludes();
        }

        private void AddCommonIncludes()
        {
            AddInclude(b => b.Include(b => b.Publisher)!);
            AddInclude(b => b.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author!));
            AddInclude(b => b.Include(b => b.BookCategories).ThenInclude(bc => bc.Category!));
        }
    }
}
