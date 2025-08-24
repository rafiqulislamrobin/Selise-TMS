using MediatR;

namespace TMS.Application.features.User.Get
{
    public record GetUserQuery(ResourseParameter resource) : IRequest<List<UserResponse>>;
}
