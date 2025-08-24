using MediatR;

namespace TMS.Application.features.Team.Put
{
    public class PutTeamCommand : IRequest<PutTeamResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}