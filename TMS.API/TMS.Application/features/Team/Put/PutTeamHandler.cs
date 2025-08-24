using MediatR;
using TMS.Application.Common.infra;
using TMS.Application.Entities;

namespace TMS.Application.features.Team.Put
{
    public class PutTeamHandler : IRequestHandler<PutTeamCommand, PutTeamResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public PutTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PutTeamResponse> Handle(PutTeamCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.Repository<Entities.Team, int>();
            var team = await repo.GetByIdAsync(request.Id);
            if (team == null)
                return new PutTeamResponse { Success = false };
            team.Update(request.Name, request.Description);
            repo.Update(team);
            await _unitOfWork.CommitAsync();
            return new PutTeamResponse { Success = true, Name = team.Name, Description = team.Description };
        }
    }
}