using MediatR;
using TMS.Application.Common.infra;
using TMS.Application.Entities;

namespace TMS.Application.features.Task.Delete
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, DeleteTaskResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteTaskHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DeleteTaskResponse> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Entities.Task, int>();
            var task = await repo.GetByIdAsync(request.Id);
            if (task == null)
                return new DeleteTaskResponse { Success = false, Message = "Task not found." };
            repo.Delete(task);
            await _unitOfWork.CommitAsync();
            return new DeleteTaskResponse { Success = true, Message = "Task deleted successfully." };
        }
    }
}