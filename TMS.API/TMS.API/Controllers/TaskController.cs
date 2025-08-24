using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TMS.Application.features.Task.Delete;
using TMS.Application.features.Task.Get;
using TMS.Application.features.Task.Post;
using TMS.Application.features.Task.Put;

namespace WebApplication1.Controllers
{
    [Route("TMS/api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TaskController> _logger;

        public TaskController(IMediator mediator, ILogger<TaskController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllTasks([FromQuery] ResourseParameter resource)
        {
            _logger.LogInformation("Getting all tasks with resource: {@Resource}", resource);
            var resourseParameter = new ResourseParameter();
            var response = await _mediator.Send(new GetTaskQuery(resourseParameter));
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            _logger.LogInformation("Creating task with title: {Title}", command.Title);
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllTasks), new { }, response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            _logger.LogInformation("Updating task with id: {Id}", id);
            var response = await _mediator.Send(command);
            if (response == null || !response.Success)
                return NotFound();
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            _logger.LogInformation("Deleting task with id: {Id}", id);
            var response = await _mediator.Send(new DeleteTaskCommand(id));
            if (response == null || !response.Success)
                return NotFound(response?.Message);
            return Ok(response);
        }
    }
}
