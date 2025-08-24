using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TMS.Application.features.User.Delete;
using TMS.Application.features.User.Get;
using TMS.Application.features.User.Post;
using TMS.Application.features.User.Put;

namespace WebApplication1.Controllers
{
    [Route("TMS/api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("", Name = "GetUsers")]
        public async Task<IActionResult> GetUser([FromQuery] ResourseParameter resource)
        {
            _logger.LogInformation("Getting users with resource: {@Resource}", resource);
            var response = await _mediator.Send(new GetUserQuery(resource));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            _logger.LogInformation("Creating user with email: {Email}", command.Email);
            var response = await _mediator.Send(command);
            return CreatedAtRoute("GetUsers", new { }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            _logger.LogInformation("Updating user with id: {Id}", id);
            var response = await _mediator.Send(command);
            if (response == null || !response.Success)
                return NotFound();
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            _logger.LogInformation("Deleting user with id: {Id}", id);
            var response = await _mediator.Send(new DeleteUserCommand(id));
            if (response == null || !response.Success)
                return NotFound(response?.Message);
            return Ok(response);
        }
    }
}
