namespace TMS.Application.features.Team.Get
{
    public interface IRepository
    {
        List<Entities.Team> GetTeams(ResourseParameter resourse);
    }
}
