using MediatR;
using TMS.Application.features.User.Get;

namespace TMS.Application.features.Team.Get
{
    public record GetTeamQuery(ResourseParameter resource) : IRequest<List<TeamResponse>>;
}