using TMS.Application.Common.infra;
using MediatR;
using BCrypt.Net;

namespace TMS.Application.features.User.Post
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IRepository _repository;
        public CreateUserHandler(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Hash the password before saving
            var user = new Entities.User();
            user.Create(request.FullName, request.Email, request.Role, request.pasword);
            var result = await _repository.CreateUser(user);

            return new CreateUserResponse
            {
                Name = user.UserName,
                Email = user.Email,
                Role = user.Role,
            };
        }
    }
}