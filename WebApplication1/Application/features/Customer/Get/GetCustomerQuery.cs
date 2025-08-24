using MediatR;

namespace Application.features.Customer.Get
{
    public record GetCustomerQuery(ResourseParameter resource) : IRequest<List<CustomerResponse>>;
}
