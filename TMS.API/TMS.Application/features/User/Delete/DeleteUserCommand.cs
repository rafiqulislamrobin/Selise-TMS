using MediatR;

namespace TMS.Application.features.User.Delete
{
    public class DeleteUserCommand : IRequest<DeleteUserResponse>
    {
        public string Id { get; set; }
        public DeleteUserCommand(string id)
        {
            Id = id;
        }
    }
}