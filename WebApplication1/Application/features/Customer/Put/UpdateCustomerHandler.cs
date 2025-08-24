using Application.Common.infra;
using MediatR;

namespace Application.features.Customer.Put
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCustomerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UpdateCustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.Repository<Entities.Customer, int>().GetByIdAsync(request.Id);
            if (customer == null)
            {
                return new UpdateCustomerResponse { Success = false };
            }
            customer.Update(request.Name, request.Remarks);


            _unitOfWork.Repository<Entities.Customer, int>().Update(customer);

            if (await _unitOfWork.CommitAsync() > 0)
            {
                return new UpdateCustomerResponse
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Remarks = customer.Remarks,
                    Success = true
                };
            }
            else
            {
                throw new InvalidOperationException("Failed to create customer.");

            }
        }
    }
}