using TMS.Application.Common.infra;
using MediatR;
using BCrypt.Net;

namespace TMS.Application.features.User.Put
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
    {
        private readonly IRepository _repository;
        public UpdateUserHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);
            if (user == null)
            {
                return new UpdateUserResponse
                {
                    Success = false
                };
            }


            user.Update(request.FullName, request.Email, request.Role);

            // Save changes using repository (if needed, e.g., for EF tracking)
            _repository.Update(user);

            return new UpdateUserResponse
            {
                FullName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                Success = true
            };
        }
    }
}