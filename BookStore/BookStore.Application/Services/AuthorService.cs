using BookStore.Application.Abstract;
using BookStore.Application.Abstract.Services;
using BookStore.Domain.Common.Pagination;
using BookStore.Domain.Exceptions;
using BookStore.Domain.Models.Author;

namespace BookStore.Application.Services
{
    public class AuthorService : IAuthorService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public AuthorService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public async Task<bool> CreateAsync(Author author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            await UnitOfWork.AuthorRepository.AddAsync(author);

            return SaveChangesAndCheckResult();
        }

        public async Task<bool> DeleteAsync(int authorId)
        {
            var entity = await UnitOfWork.AuthorRepository.GetByIdAsync(authorId);

            if (entity == null)
                throw new EntityNotFoundException("Author not found");

            UnitOfWork.AuthorRepository.Delete(entity);

            return SaveChangesAndCheckResult();
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            var entities = await UnitOfWork.AuthorRepository.GetAllAsync();

            return entities;
        }
        public async Task<PagedList<Author>> GetPagedListAsync(AuthorParameters parameters)
        {
            var entities = await UnitOfWork.AuthorRepository.GetAllAsync();
            var query = entities.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                query = query.Where(a => a.LastName.Contains(parameters.SearchTerm));
            }

            query = parameters.OrderBy?.ToLower() switch
            {
                "firstname" => query.OrderBy(a => a.FirstName),
                "lastname" => query.OrderBy(a => a.LastName),
                _ => query.OrderBy(a => a.Id)
            };

            var result = PagedList<Author>.ToPagedList(query,
                parameters.PageNumber,
                parameters.PageSize);

            return result;
        }

        public async Task<Author> GetByIdAsync(int authorId)
        {
            var entity = await UnitOfWork.AuthorRepository.GetByIdAsync(authorId);

            if (entity == null)
                throw new EntityNotFoundException("Author not found");

            return entity;
        }

        public async Task<bool> UpdateAsync(Author author)
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author));

            var entity = await UnitOfWork.AuthorRepository
                .GetByIdAsync(author.Id);

            if (entity == null)
                throw new EntityNotFoundException("Author not found");

            entity.FirstName = author.FirstName ?? entity.FirstName;
            entity.LastName = author.LastName ?? entity.LastName;
            entity.Bio = author.Bio ?? entity.Bio;

            UnitOfWork.AuthorRepository.Update(entity);

            return SaveChangesAndCheckResult();
        }                                                                                                                                   
        private bool SaveChangesAndCheckResult()
        {
            var result = UnitOfWork.Save();
            return result > 0;
        }
    }
}
