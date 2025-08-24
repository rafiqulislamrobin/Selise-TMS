using MediatR;
using TMS.Application.Common.Enum;
using TMS.Application.Common.infra;
using TaskStatus = TMS.Application.Common.Enum.TaskStatus;

namespace TMS.Application.features.Task.Post
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, CreateTaskResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateTaskHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CreateTaskResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new Entities.Task(
                request.Title,
                request.Description,
                (TaskStatus)request.Status,
                request.AssignedToUserId,
                request.CreatedUserId,
                request.TeamId,
                request.DueDate
            );
            await _unitOfWork.Repository<Entities.Task, int>().AddAsync(task);
            await _unitOfWork.CommitAsync();
            return new CreateTaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = (int)task.Status,
                AssignedToUserId = task.AssignedToUserId,
                CreatedUserId = task.CreatedUserId,
                TeamId = task.TeamId,
                DueDate = task.DueDate
            };
        }
    }
}