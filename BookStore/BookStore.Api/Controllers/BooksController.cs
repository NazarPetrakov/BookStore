using AutoMapper;
using BookStore.Application.Abstract.Services;
using BookStore.Application.Common.Identity;
using BookStore.Application.Contracts.Book;
using BookStore.Application.Contracts.Review;
using BookStore.Domain.Common.Pagination;
using BookStore.Domain.Models.Book;
using BookStore.Domain.Models.Review;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BookStore.Api.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public BooksController(
            IBookService bookService,
            IReviewService reviewService,
            IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
            _reviewService = reviewService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] BookParameters parameters)
        {
            return await GetBooksAsync(() => _bookService.GetPagedListAsync(parameters));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _bookService.GetByIdAsync(id);

            var book = _mapper.Map<GetBook>(entity);

            return Ok(book);
        }

        [HttpGet("by-author/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByAuthor(int id, [FromQuery]BookParameters parameters)
        {
            return await GetBooksAsync(() => _bookService.GetByAuthorPagedListAsync(id, parameters));
        }

        [HttpGet("by-category/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByCategory(int id, [FromQuery] BookParameters parameters)
        {
            return await GetBooksAsync(() => _bookService.GetByCategoryPagedListAsync(id, parameters));
        }

        [HttpGet("by-publisher/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByPublisher(int id, [FromQuery] BookParameters parameters)
        {
            return await GetBooksAsync(() => _bookService.GetByPublisherPagedListAsync(id, parameters));
        }

        [Authorize(Roles = UserRoles.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isBookDeleted = await _bookService.DeleteAsync(id);

            if (isBookDeleted)
                return Ok(isBookDeleted);
            else
                return BadRequest();
        }

        [Authorize(Roles = UserRoles.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBook createBook)
        {
            var bookEntity = _mapper.Map<Book>(createBook);

            var isBookrCreated = await _bookService
                .CreateAsync(bookEntity, 
                    createBook.CategoryIds, createBook.AuthorIds, createBook.PublisherId);

            if (isBookrCreated)
                return CreatedAtAction("Create",
                    new { id = bookEntity.Id, Book = createBook });
            else
                return BadRequest();
        }

        [Authorize(Roles = UserRoles.User, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("{id}/reviews")]
        public async Task<IActionResult> CreateReview([FromBody] CreateReview createReview, int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reviewEntity = _mapper.Map<Review>(createReview);

            var isReviewCreated = await _reviewService
                .CreateAsync(reviewEntity, id, userId);

            if (isReviewCreated)
                return CreatedAtAction("Create",
                    new { id = reviewEntity.Id, Review = createReview });
            else
                return BadRequest();
        }

        [Authorize(Roles = UserRoles.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBook updateBook)
        {
            var book = _mapper.Map<Book>(updateBook);

            book.Id = id;

            var isBookCreated = await _bookService
                .UpdateAsync(book, 
                    updateBook.CategoryIds, updateBook.AuthorIds, updateBook.PublisherId);

            if (isBookCreated)
                return Ok(isBookCreated);
            else
                return BadRequest();
        }
        private async Task<IActionResult> GetBooksAsync(Func<Task<PagedList<Book>>> getBooksFunc)
        {
            var entities = await getBooksFunc();

            var metadata = new
            {
                entities.TotalCount,
                entities.PageSize,
                entities.CurrentPage,
                entities.TotalPages,
                entities.HasNext,
                entities.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var books = _mapper.Map<ICollection<GetBook>>(entities);

            return Ok(books);
        }
    }
}
