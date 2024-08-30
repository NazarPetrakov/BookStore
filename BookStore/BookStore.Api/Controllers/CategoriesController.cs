using AutoMapper;
using BookStore.Application.Abstract.Services;
using BookStore.Application.Common.Identity;
using BookStore.Application.Contracts.Category;
using BookStore.Domain.Models.Category;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.Api.Controllers
{
    [Authorize(Roles = UserRoles.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] CategoryParameters parameters)
        {
            var entities = await _categoryService.GetPagedListAsync(parameters);

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

            var categories = _mapper.Map<ICollection<GetCategory>>(entities);

            return Ok(categories);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _categoryService.GetByIdAsync(id);

            var category = _mapper.Map<GetCategory>(entity);

            return Ok(category);
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
