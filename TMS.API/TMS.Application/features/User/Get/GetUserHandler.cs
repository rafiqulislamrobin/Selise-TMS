using MediatR;
using SystemTask = System.Threading.Tasks.Task;

namespace TMS.Application.features.User.Get
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, List<UserResponse>>
    {
        private readonly IRepository _repository;

        public GetUserHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var Users = _repository.GetUsers(request.resource);
            var result = Users.Select(c => new UserResponse
            {
                Name = c.UserName,
                Email = c.Email,
                Role = c.Role,
                Id = c.Id
            }).ToList();
            return await SystemTask.FromResult(result);
        }
    }
}
