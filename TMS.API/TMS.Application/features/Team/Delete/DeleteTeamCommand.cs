using MediatR;

namespace TMS.Application.features.Team.Delete
{
    public class DeleteTeamCommand : IRequest<DeleteTeamResponse>
    {
        public int Id { get; set; }
        public DeleteTeamCommand(int id)
        {
            Id = id;
        }
    }
}