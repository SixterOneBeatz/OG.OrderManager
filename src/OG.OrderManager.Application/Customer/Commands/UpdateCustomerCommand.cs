using AutoMapper;
using MediatR;
using OG.OrderManager.Application.Common.Interfaces;
using OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Application.Customer.Commands
{
    public class UpdateCustomerCommand : IRequest<TransactionCustomerResponse>
    {
        public CustomerDTO Customer { get; set; }

        public UpdateCustomerCommand(CustomerDTO customer)
            => Customer = customer;
    }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, TransactionCustomerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TransactionCustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetCustomer(request.Customer.Id);

            _unitOfWork.CustomerRepository.UpdateCustomer(customer);
            _mapper.Map(request.Customer, customer);

            await _unitOfWork.Complete();

            return new()
            {
                Id = customer.Id
            };
        }
    }
}
