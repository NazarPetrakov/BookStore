using BookStore.Application.Abstract.Repositories;

namespace BookStore.Application.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IPublisherRepository PublisherRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IBookRepository BookRepository { get; }
        IBookCategoryRepository BookCategoryRepository { get; }
        IBookAuthorRepository BookAuthorRepository { get; }
        IReviewRepository ReviewRepository { get; }

        int Save();
    }
}
