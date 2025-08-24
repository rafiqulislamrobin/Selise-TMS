namespace Application.features.Customer.Get
{
    public interface IRepository
    {
        List<Entities.Customer> GetCustomers(ResourseParameter resourse);
    }
}
