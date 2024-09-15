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

        public ICategoryRepository CategoryRepository { get; }
        public IBookRepository BookRepository { get; }

        public IBookCategoryRepository BookCategoryRepository { get; }

        public IBookAuthorRepository BookAuthorRepository { get; }
        public IReviewRepository ReviewRepository { get; }

        public UnitOfWork(AppDbContext dbContext,
                        IPublisherRepository publisherRepository,
                        IAuthorRepository authorRepository,
                        ICategoryRepository categoryRepository,
                        IBookRepository bookRepository,
                        IBookCategoryRepository bookCategoryRepository,
                        IBookAuthorRepository bookAuthorRepository,
                        IReviewRepository reviewRepository)
        {
            _dbContext = dbContext;
            PublisherRepository = publisherRepository;
            AuthorRepository = authorRepository;
            CategoryRepository = categoryRepository;
            BookRepository = bookRepository;
            BookCategoryRepository = bookCategoryRepository;
            BookAuthorRepository = bookAuthorRepository;
            ReviewRepository = reviewRepository;
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
