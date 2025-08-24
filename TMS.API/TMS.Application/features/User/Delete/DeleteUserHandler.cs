using MediatR;
using TMS.Application.Common.infra;
using TMS.Application.Entities;

namespace TMS.Application.features.User.Delete
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
    {
        private readonly IRepository _repository;
        public DeleteUserHandler(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);
            if (user == null)
            {
                return new DeleteUserResponse { Success = false, Message = "User not found." };
            }
            _repository.Delete(user);
            return new DeleteUserResponse { Success = true, Message = "User deleted successfully." };
        }
    }
}