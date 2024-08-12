using AutoMapper;
using BookStore.Application.Abstract.Services;
using BookStore.Application.Contracts.Book;
using BookStore.Domain.Models.Book;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _bookService.GetAllAsync();

            var books = _mapper.Map<ICollection<GetBook>>(entities);

            return Ok(books);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _bookService.GetByIdAsync(id);

            var book = _mapper.Map<GetBook>(entity);

            return Ok(book);
        }
        [HttpGet("by-author/{id}")]
        public async Task<IActionResult> GetByAuthor(int id)
        {
            var entities = await _bookService.GetByAuthorAsync(id);

            var books = _mapper.Map< ICollection<GetBook>>(entities);

            return Ok(books);
        }
        [HttpGet("by-category/{id}")]
        public async Task<IActionResult> GetByCategory(int id)
        {
            var entities = await _bookService.GetByCategoryAsync(id);

            var books = _mapper.Map<ICollection<GetBook>>(entities);

            return Ok(books);
        }
        [HttpGet("by-publisher/{id}")]
        public async Task<IActionResult> GetByPublisher(int id)
        {
            var entities = await _bookService.GetByPublisherAsync(id);

            var books = _mapper.Map<ICollection<GetBook>>(entities);

            return Ok(books);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isBookDeleted = await _bookService.DeleteAsync(id);

            if (isBookDeleted)
                return Ok(isBookDeleted);
            else
                return BadRequest();
        }
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

    }
}
