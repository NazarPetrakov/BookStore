using AutoMapper;
using BookStore.Application.Abstract.Services;
using BookStore.Application.Contracts.Book;
using BookStore.Application.Contracts.Publisher;
using BookStore.Domain.Models.Publisher;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
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
        public async Task<IActionResult> GetAll()
        {
            var entityList = await _publisherService.GetAllAsync();

            var publisherList = _mapper.Map<IEnumerable<GetPublisher>>(entityList);

            return Ok(publisherList);
        }
        [HttpGet("{id}/books")]
        public async Task<IActionResult> GetBooksByPublisherId(int id)
        {
            var entityList = await _publisherService.GetBooksByPublisherAsync(id);

            var bookList = _mapper.Map<IEnumerable<GetBook>>(entityList);

            return Ok(bookList);
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _publisherService.GetByIdAsync(id);

            var publisher = _mapper.Map<GetPublisher>(entity);

            return Ok(publisher);
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
