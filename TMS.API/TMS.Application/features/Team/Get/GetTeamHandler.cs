using MediatR;
using TMS.Application.Common.infra;
using TMS.Application.Entities;
using TMS.Application.features.User.Get;

namespace TMS.Application.features.Team.Get
{
    public class GetTeamHandler : IRequestHandler<GetTeamQuery, List<TeamResponse>>
    {
        private readonly IRepository _repository;
        public GetTeamHandler(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<TeamResponse>> Handle(GetTeamQuery request, CancellationToken cancellationToken)
        {
            var teams = _repository.GetTeams(request.resource);
            var result = teams.Select(t => new TeamResponse
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description
            }).ToList();
            return await System.Threading.Tasks.Task.FromResult(result);
        }
    }
}