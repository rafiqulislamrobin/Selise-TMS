namespace TMS.Application.features.User.Delete
{
    public interface IRepository
    {
        Task<Entities.User> GetByIdAsync(string id);
        void Delete(Entities.User user);

    }
}
