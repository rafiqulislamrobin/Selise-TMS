using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TMS.Application.features.User.Login;
using TMS.Application.features.User.Post;

namespace WebApplication1.Controllers
{
    [Route("TMS/api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IMediator mediator, ILogger<LoginController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            _logger.LogInformation("Login attempt for email: {Email}", command.Email);
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
