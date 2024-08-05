using BookStore.Application.Abstract;
using BookStore.Application.Abstract.Repositories;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public IPublisherRepository PublisherRepository { get; }

        public IAuthorRepository AuthorRepository { get; }

        public UnitOfWork(AppDbContext dbContext,
                            IPublisherRepository publisherRepository,
                            IAuthorRepository authorRepository)
        {
            _dbContext = dbContext;
            PublisherRepository = publisherRepository;
            AuthorRepository = authorRepository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
