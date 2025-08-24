using MediatR;

namespace Application.features.Customer.Put
{
    public class UpdateCustomerCommand : IRequest<UpdateCustomerResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
    }
}