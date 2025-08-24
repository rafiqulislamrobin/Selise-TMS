namespace TMS.Application.features.User.Get
{
    public interface IRepository
    {
        List<Entities.User> GetUsers(ResourseParameter resourse);
    }
}
