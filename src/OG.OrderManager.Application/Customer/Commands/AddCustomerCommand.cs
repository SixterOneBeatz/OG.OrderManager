using AutoMapper;
using MediatR;
using OG.OrderManager.Application.Common.Interfaces;
using OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Application.Customer.Commands
{
    public class AddCustomerCommand : IRequest<TransactionCustomerResponse>
    {
        public CustomerDTO Customer { get; set; }

        public AddCustomerCommand(CustomerDTO customer)
            => Customer = customer;
    }

    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, TransactionCustomerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TransactionCustomerResponse> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Domain.Customer>(request.Customer);

            _unitOfWork.CustomerRepository.AddCustomer(customer);
            await _unitOfWork.Complete();

            return new()
            {
                Id = customer.Id
            };
        }
    }
}
