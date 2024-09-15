using BookStore.Application.Abstract.Services;
using BookStore.Application.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/v1/authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        public AuthenticateController(
            IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _authenticateService.LoginAsync(model);
            if (response.Status == "Error")
                return Unauthorized(response.Message);
            return Ok(new
            {
                token = response.Token,
                expiration = response.Expiration
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var response = await _authenticateService.RegisterUserAsync(model);
            if (response.Status == "Error")
                return StatusCode(StatusCodes.Status500InternalServerError, response);

            return Ok(response);
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var response = await _authenticateService.RegisterUserAsync(model, isAdmin: true);
            if (response.Status == "Error")
                return StatusCode(StatusCodes.Status500InternalServerError, response);

            return Ok(response);
        }
    }
}
