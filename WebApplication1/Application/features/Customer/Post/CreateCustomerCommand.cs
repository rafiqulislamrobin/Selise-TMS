using MediatR;

namespace Application.features.Customer.Post
{
    public class CreateCustomerCommand : IRequest<CreateCustomerResponse>
    {
        public string Name { get; set; }
        public string Remarks { get; set; }
    }
}