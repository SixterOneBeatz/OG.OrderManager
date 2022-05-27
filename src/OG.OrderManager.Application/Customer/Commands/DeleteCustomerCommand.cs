using AutoMapper;
using MediatR;
using OG.OrderManager.Application.Common.Interfaces;
using OG.OrderManager.Application.Common.Protos;

namespace OG.OrderManager.Application.Customer.Commands
{
    public class DeleteCustomerCommand : IRequest<TransactionCustomerResponse>
    {
        public int Id { get; set; }

        public DeleteCustomerCommand(int id)
            => Id = id;
    }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, TransactionCustomerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TransactionCustomerResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetCustomer(request.Id);

            if (customer is null)
                throw new ApplicationException("Not Found");

            _unitOfWork.CustomerRepository.DeleteCustomer(customer);
            await _unitOfWork.Complete();

            return new()
            {
                Id = customer.Id
            };
        }
    }
}
