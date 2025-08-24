using MediatR;
using TMS.Application.Common.Enum;
using TMS.Application.Common.infra;
using TaskStatus = TMS.Application.Common.Enum.TaskStatus;

namespace TMS.Application.features.Task.Put
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, UpdateTaskResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateTaskHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UpdateTaskResponse> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Entities.Task, int>();
            var task = await repo.GetByIdAsync(request.Id);
            if (task == null)
                return new UpdateTaskResponse { Success = false };
            task.Update(request.Title, request.Description, (TaskStatus)request.Status, request.AssignedToUserId, request.TeamId, request.DueDate);
            repo.Update(task);
            await _unitOfWork.CommitAsync();
            return new UpdateTaskResponse
            {
                Success = true,
                Title = task.Title,
                Description = task.Description,
                Status = (int)task.Status,
                AssignedToUserId = task.AssignedToUserId,
                TeamId = task.TeamId,
                DueDate = task.DueDate
            };
        }
    }
}