using AutoMapper;
using BookStore.Application.Abstract.Services;
using BookStore.Application.Contracts.Author;
using BookStore.Application.Contracts.Book;
using BookStore.Application.Services;
using BookStore.Domain.Models.Author;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/v1/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _authorService.GetAllAsync();

            var authors = _mapper.Map<ICollection<GetAuthor>>(entities);

            return Ok(authors);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateAuthor createAuthor)
        {
            var authorEntity = _mapper.Map<Author>(createAuthor);

            var isAuthorCreated = await _authorService.CreateAsync(authorEntity);

            if (isAuthorCreated)
                return CreatedAtAction("Create",
                    new { id = authorEntity.Id, Author = createAuthor });
            else
                return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _authorService.GetByIdAsync(id);

            var author = _mapper.Map<GetAuthor>(entity);

            return Ok(author);
        }
        [HttpGet("{id}/books")]
        public async Task<IActionResult> GetBooksByAuthorId(int id)
        {
            var bookEntities = await _authorService.GetBooksByAuthorAsync(id);

            var books = _mapper.Map<IEnumerable<GetBook>>(bookEntities);

            return Ok(books);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isAuthorDeleted = await _authorService.DeleteAsync(id);

            if (isAuthorDeleted)
                return Ok(isAuthorDeleted);
            else
                return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAuthor updateAuthor)
        {
            var author = _mapper.Map<Author>(updateAuthor);

            author.Id = id;

            var isAuthorCreated = await _authorService.UpdateAsync(author);

            if (isAuthorCreated)
                return Ok(isAuthorCreated);
            else
                return BadRequest();
        }

    }
}
