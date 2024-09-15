using AutoMapper;
using BookStore.Application.Abstract.Services;
using BookStore.Application.Common.Identity;
using BookStore.Application.Contracts.Review;
using BookStore.Application.Services;
using BookStore.Domain.Common.Pagination;
using BookStore.Domain.Models.Review;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/v1/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }
        [Authorize(Roles = UserRoles.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]ReviewParameters parameters)
        {
            return await GetReviewsAsync(() => _reviewService.GetPagedListAsync(parameters));
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("by-book/{id}")]
        public async Task<IActionResult> GetByBook(int id, [FromQuery] ReviewParameters parameters)
        {
            return await GetReviewsAsync(() => _reviewService.GetByBookPagedListAsync(id, parameters));
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole(UserRoles.Admin);

            var review = await _reviewService.GetByIdAsync(id);

            if (isAdmin || review.UserId == userId)
            {
                var isReviewDeleted = await _reviewService.DeleteAsync(id);
                if (isReviewDeleted)
                    return Ok(isReviewDeleted);
                else
                    return BadRequest();
            }

            return Forbid("You are not authorized to delete this review");
        }
        private async Task<IActionResult> GetReviewsAsync(Func<Task<PagedList<Review>>> getReviewsFunc)
        {
            var entities = await getReviewsFunc();

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

            var reviews = _mapper.Map<ICollection<GetReview>>(entities);

            return Ok(reviews);
        }
    }
}
