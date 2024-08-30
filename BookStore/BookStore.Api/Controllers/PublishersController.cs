using AutoMapper;
using BookStore.Application.Abstract.Services;
using BookStore.Application.Common.Identity;
using BookStore.Application.Contracts.Publisher;
using BookStore.Domain.Models.Publisher;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.Api.Controllers
{
    [Authorize(Roles = UserRoles.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/publishers")]
    [ApiController]

    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;
        public PublishersController(IPublisherService publisherService, IMapper mapper)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery]PublisherParameters parameters)
        {
            var entities = await _publisherService.GetPagedListAsync(parameters);

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

            var publisherList = _mapper.Map<IEnumerable<GetPublisher>>(entities);

            return Ok(publisherList);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _publisherService.GetByIdAsync(id);

            var publisher = _mapper.Map<GetPublisher>(entity);

            return Ok(publisher);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePublisher createPublisher)
        {
            var publisherEntity = _mapper.Map<Publisher>(createPublisher);

            var isPublisherCreated = await _publisherService.CreateAsync(publisherEntity);

            if (isPublisherCreated)
                return CreatedAtAction("Create",
                    new { id = publisherEntity.Id, Publisher = createPublisher});
            else
                return BadRequest();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isPublisherDeleted = await _publisherService.DeleteAsync(id);

            if (isPublisherDeleted)
                return Ok(isPublisherDeleted);
            else
                return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePublisher updatePublisher)
        {
            var publisher = _mapper.Map<Publisher>(updatePublisher);

            publisher.Id = id;

            var isPublisherCreated = await _publisherService.UpdateAsync(publisher);

            if (isPublisherCreated)
                return Ok(isPublisherCreated);
            else
                return BadRequest();
        }
    }
}
