﻿using BookStore.Application.Abstract;
using BookStore.Application.Abstract.Services;
using BookStore.Application.Common.Specifications.Book;
using BookStore.Domain.Common.Pagination;
using BookStore.Domain.Exceptions;
using BookStore.Domain.Models.Book;
using BookStore.Domain.Models.JoinEntities;

namespace BookStore.Application.Services
{
    public class BookService : IBookService
    {
        IUnitOfWork UnitOfWork { get; set; }
        public BookService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var spec = new DetailedBookSpec();

            var entities = await UnitOfWork.BookRepository.GetAsync(spec);

            return entities;
        }
        public async Task<Book> GetByIdAsync(int bookId)
        {
            var spec = new DetailedBookSpec(bookId);

            var entity = await UnitOfWork.BookRepository.GetEntityWithSpec(spec);

            if (entity == null)
                throw new EntityNotFoundException("Book not found");

            return entity;
        }
        public Task<PagedList<Book>> GetPagedListAsync(BookParameters parameters)
        {
            var spec = new DetailedBookSpec();
            return GetBooksBySpecAsync(spec, parameters);
        }

        public async Task<PagedList<Book>> GetByAuthorPagedListAsync(int authorId, BookParameters parameters)
        {
            var spec = new BooksByAuthorSpec(authorId);
            return await GetBooksBySpecAsync(spec, parameters);
        }

        public async Task<PagedList<Book>> GetByCategoryPagedListAsync(int categoryId, BookParameters parameters)
        {
            var spec = new BooksByCategorySpec(categoryId);
            return await GetBooksBySpecAsync(spec, parameters);
        }

        public async Task<PagedList<Book>> GetByPublisherPagedListAsync(int publisherId, BookParameters parameters)
        {
            var spec = new BooksByPublisherSpec(publisherId);
            return await GetBooksBySpecAsync(spec, parameters);
        }
       
        public async Task<bool> CreateAsync(Book book,
            int[] categoryIds, int[] authorIds, int? publisherId)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (categoryIds == null || categoryIds.Length == 0)
                throw new ArgumentException("Category IDs cannot be null or empty", nameof(categoryIds));
            if (authorIds == null || authorIds.Length == 0)
                throw new ArgumentException("Author IDs cannot be null or empty", nameof(authorIds));

            if (publisherId.HasValue)
            {
                var publisher = await UnitOfWork.PublisherRepository
                    .GetByIdAsync(publisherId.Value);
                if (publisher == null)
                    throw new EntityNotFoundException("Publisher not found");

                book.Publisher = publisher;
                book.PublisherId = publisherId;
            }

            var categories = await UnitOfWork.CategoryRepository.GetByIdsAsync(categoryIds);
            var authors = await UnitOfWork.AuthorRepository.GetByIdsAsync(authorIds);

            if (categories.Count() != categoryIds.Length)
                throw new EntityNotFoundException("One or more categories not found");
            if (authors.Count() != authorIds.Length)
                throw new EntityNotFoundException("One or more authors not found");

            book.BookCategories = categoryIds.Select(id => new BookCategory
            {
                Book = book,
                BookId = book.Id,
                CategoryId = id
            }).ToList();
            book.BookAuthors = authorIds.Select(id => new BookAuthor
            {
                Book = book,
                BookId = book.Id,
                AuthorId = id
            }).ToList();

            await UnitOfWork.BookRepository.AddAsync(book);

            return SaveChangesAndCheckResult();
        }

        public async Task<bool> DeleteAsync(int bookId)
        {
            var book = await UnitOfWork.BookRepository.GetByIdAsync(bookId);

            if (book == null)
                throw new EntityNotFoundException("Book not found");

            UnitOfWork.BookRepository.Delete(book);

            return SaveChangesAndCheckResult();
        }
        public async Task<bool> UpdateAsync(Book book,
            int[]? categoryIds = null, int[]? authorIds = null, int? publisherId = null)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            var bookEntity = await UnitOfWork.BookRepository
                .GetByIdAsync(book.Id);
            if (bookEntity == null)
                throw new EntityNotFoundException("Book not found");

            UpdateBookDetails(book, bookEntity);

            if (publisherId.HasValue)
                await UpdateBookPublisherAsync(bookEntity, publisherId.Value);
            if (categoryIds != null && categoryIds.Length > 0)
                await UpdateCategoriesAsync(bookEntity, categoryIds);
            if (authorIds != null && authorIds.Length > 0)
                await UpdateAuthorsAsync(bookEntity, authorIds);

            UnitOfWork.BookRepository.Update(bookEntity);
            return SaveChangesAndCheckResult();
        }
        private async Task<PagedList<Book>> GetBooksBySpecAsync(ISpecification<Book> spec, BookParameters parameters)
        {
            var books = await UnitOfWork.BookRepository.GetAsync(spec);

            var query = books.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.SearchTerm))
            {
                query = query.Where(b => b.Title.Contains(parameters.SearchTerm)
                    || b.ISBN.Contains(parameters.SearchTerm));
            }

            if (parameters.MinPrice <= parameters.MaxPrice)
            {
                query = query.Where(b => b.Price >= parameters.MinPrice && b.Price <= parameters.MaxPrice);
            }

            if (parameters.MinPublicationYear <= parameters.MaxPublicationYear)
            {
                query = query.Where(b => b.PublicationYear >= parameters.MinPublicationYear
                    && b.PublicationYear <= parameters.MaxPublicationYear);
            }

            if (!string.IsNullOrEmpty(parameters.OrderBy))
            {
                query = parameters.OrderBy.ToLower() switch
                {
                    "title" => query.OrderBy(b => b.Title),
                    "price" => query.OrderBy(b => b.Price),
                    "year" or "publicationyear" or "date" or "publicationdate"
                        => query.OrderBy(b => b.PublicationYear),
                    _ => query
                };
            }

            return PagedList<Book>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
        private static void UpdateBookDetails(Book book, Book bookToUpdate)
        {
            if (!string.IsNullOrEmpty(book.Title))
                bookToUpdate.Title = book.Title;
            if (!string.IsNullOrEmpty(book.ISBN))
                bookToUpdate.ISBN = book.ISBN;
            if (book.PublicationYear != default)
                bookToUpdate.PublicationYear = book.PublicationYear;
            if (book.Price != default)
                bookToUpdate.Price = book.Price;
            if (!string.IsNullOrEmpty(book.Description))
                bookToUpdate.Description = book.Description;
        }

        private async Task UpdateBookPublisherAsync(Book bookToUpdate, int publisherId)
        {
            var publisher = await UnitOfWork.PublisherRepository.GetByIdAsync(publisherId);
            if (publisher == null)
                throw new EntityNotFoundException("Publisher not found");

            bookToUpdate.Publisher = publisher;
            bookToUpdate.PublisherId = publisherId;
        }

        private async Task UpdateCategoriesAsync(Book bookToUpdate, int[] categoryIds)
        {
            var categories = await UnitOfWork.CategoryRepository.GetByIdsAsync(categoryIds);
            if (categories.Count() != categoryIds.Length)
                throw new EntityNotFoundException("One or more categories not found");

            var bookCategories = await UnitOfWork.BookCategoryRepository.GetAllAsync();

            var bookCategoriesByBook = bookCategories
                .Where(c => c.BookId == bookToUpdate.Id);

            UnitOfWork.BookCategoryRepository.RemoveRange(bookCategoriesByBook);

            var newCategories = categoryIds.Select(id => new BookCategory
            {
                BookId = bookToUpdate.Id,
                CategoryId = id
            }).ToList();

            UnitOfWork.BookCategoryRepository.AddRange(newCategories);
        }

        private async Task UpdateAuthorsAsync(Book bookToUpdate, int[] authorIds)
        {
            var authors = await UnitOfWork.AuthorRepository.GetByIdsAsync(authorIds);
            if (authors.Count() != authorIds.Length)
                throw new EntityNotFoundException("One or more authors not found");

            var bookAuthors = await UnitOfWork.BookAuthorRepository.GetAllAsync();

            var bookAuthorsByBook = bookAuthors
                .Where(ba => ba.BookId == bookToUpdate.Id);

            UnitOfWork.BookAuthorRepository.RemoveRange(bookAuthorsByBook);

            var newAuthors = authorIds.Select(id => new BookAuthor
            {
                BookId = bookToUpdate.Id,
                AuthorId = id
            }).ToList();

            UnitOfWork.BookAuthorRepository.AddRange(newAuthors);
        }
        private bool SaveChangesAndCheckResult()
        {
            var result = UnitOfWork.Save();
            return result > 0;
        }


    }
}
