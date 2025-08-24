using MediatR;

namespace TMS.Application.features.Team.Post
{
    public class CreateTeamCommand : IRequest<CreateTeamResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}