using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.features.Customer.Get
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, List<CustomerResponse>>
    {
        private readonly IRepository _repository;

        public GetCustomerHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CustomerResponse>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customers = _repository.GetCustomers(request.resource);
            var result = customers.Select(c => new CustomerResponse
            {
                Id = c.Id,
                Name = c.Name,
                Remarks = c.Remarks
            }).ToList();
            return await Task.FromResult(result);
        }
    }
}
