namespace TMS.Application.features.User.Put
{
    public interface IRepository
    {
        bool Update(Entities.User user);
        Task<Entities.User> GetByIdAsync(string id);
    } 
}
