namespace TMS.Application.features.User.Post
{
    public interface IRepository
    {
        System.Threading.Tasks.Task AddAsync(Entities.User user);
        System.Threading.Tasks.Task<bool> CreateUser(Entities.User user);
    }
}
