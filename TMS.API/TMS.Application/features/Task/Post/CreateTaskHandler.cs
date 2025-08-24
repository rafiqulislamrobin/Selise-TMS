using MediatR;
using TMS.Application.Common.Enum;
using TMS.Application.Common.infra;
using TaskStatus = TMS.Application.Common.Enum.TaskStatus;

namespace TMS.Application.features.Task.Post
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, CreateTaskResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository _repo;
        public CreateTaskHandler(IUnitOfWork unitOfWork, IRepository repo)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
        }
        public async Task<CreateTaskResponse?> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var team = _repo.GetTeamById(request.TeamId);
            if(team == null)
            {
                return null;
            }
            var user = _repo.GetUserId(request.AssignedToUserId);
            if (user == null)
            {
                return null;
            }
            var task = new Entities.Task(
                request.Title,
                request.Description,
                (TaskStatus)request.Status,
                request.AssignedToUserId,
                request.CreatedUserId,
                request.TeamId,
                request.DueDate
            );

            try
            {
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
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}