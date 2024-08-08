using AutoMapper;
using BookStore.Application.Abstract.Services;
using BookStore.Application.Contracts.Book;
using BookStore.Application.Contracts.Category;
using BookStore.Domain.Models.Category;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/v1/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _categoryService.GetAllAsync();

            var categories = _mapper.Map<ICollection<GetCategory>>(entities);

            return Ok(categories);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategory createCategory)
        {
            var categoryEntity = _mapper.Map<Category>(createCategory);

            var isCategoryCreated = await _categoryService.CreateAsync(categoryEntity);

            if (isCategoryCreated)
                return CreatedAtAction("Create",
                    new { id = categoryEntity.Id, Category = createCategory });
            else
                return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _categoryService.GetByIdAsync(id);

            var category = _mapper.Map<GetCategory>(entity);

            return Ok(category);
        }
        [HttpGet("{id}/books")]
        public async Task<IActionResult> GetBooksByCategoryId(int id)
        {
            var bookEntities = await _categoryService.GetBooksByCategoryAsync(id);

            var books = _mapper.Map<IEnumerable<GetBook>>(bookEntities);

            return Ok(books);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isCategoryDeleted = await _categoryService.DeleteAsync(id);

            if (isCategoryDeleted)
                return Ok(isCategoryDeleted);
            else
                return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategory updateCategory)
        {
            var category = _mapper.Map<Category>(updateCategory);

            category.Id = id;

            var isCategoryCreated = await _categoryService.UpdateAsync(category);

            if (isCategoryCreated)
                return Ok(isCategoryCreated);
            else
                return BadRequest();
        }
    }
}
