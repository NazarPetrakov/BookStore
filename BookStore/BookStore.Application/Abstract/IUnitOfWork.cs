using BookStore.Application.Abstract.Repositories;

namespace BookStore.Application.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IPublisherRepository PublisherRepository { get; }
        IAuthorRepository AuthorRepository { get; }

        int Save();
    }
}
