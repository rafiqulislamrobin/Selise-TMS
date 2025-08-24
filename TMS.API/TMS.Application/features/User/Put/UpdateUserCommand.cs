using MediatR;

namespace TMS.Application.features.User.Put
{
    public class UpdateUserCommand : IRequest<UpdateUserResponse>
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}